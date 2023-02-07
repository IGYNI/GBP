using System.Collections.Generic;
using Player.Commands;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
	public enum PlayerState
	{
		Idle,
		Run,
		TakeItem,
		Interact,
		UseItem,
		IdleInteract,
	}

	public float itemOutlineWidth;

	public IObservableVar<PlayerState> State => _playerState;
	[SerializeField] private LayerMask groundLayer;
	[SerializeField] private LayerMask interactable;

	public IObservableVar<ItemInteraction> HoveredItem => _hoveredItem;
	public IReadOnlyCollection<IPlayerAction> Actions => _playerActions;
	
	private readonly ObservableVar<ItemInteraction> _hoveredItem = new ObservableVar<ItemInteraction>();

	private PlayerState _previousState;
	private NavMeshAgent _navMeshAgent;
	private IPlayerAction _currentAction;
	private readonly Queue<IPlayerAction> _playerActions = new Queue<IPlayerAction>();
	private readonly Idle _idleAction = new Idle();
	private Camera _raycastCamera;

	private readonly ObservableVar<PlayerState> _playerState = new ObservableVar<PlayerState>();

	private void Awake()
	{
		_navMeshAgent = GetComponent<NavMeshAgent>();
	}

	private void Start()
	{
		_raycastCamera = Camera.main;
		_currentAction = _idleAction;
	}
	
	public void SetDestination(Vector3 destination)
	{
		_navMeshAgent.destination = destination;
	}

	private void Update()
	{
		UpdateNavigation();
		CheckItems();
		UpdateActions();
	}

	private void UpdateNavigation()
	{
		if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
			return;
		if (_raycastCamera == null)
		{
			_raycastCamera = Camera.main;
		}
		if (_raycastCamera == null)
			return;
		
		if (State.Value != PlayerState.Idle && State.Value != PlayerState.Run) return;
		if (Input.GetMouseButtonDown(0) && _hoveredItem.Value == null)
		{
			Ray mRay = _raycastCamera.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(mRay.origin, mRay.direction, out RaycastHit hitInfo, 100, groundLayer))
			{
				if (Vector3.Distance(hitInfo.point, transform.position) < 1f)
					return;
				
				NavMeshPath navMeshPath = new NavMeshPath();
				if (_navMeshAgent.CalculatePath(hitInfo.point, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
				{
					//Debug.Log(hitInfo.transform.gameObject.name);
					_playerActions.Clear();
					_currentAction = _idleAction;
					_playerActions.Enqueue(new MoveToPoint(this, hitInfo.point));
				}
			}
		}
	}

	private void CheckItems()
	{
		if (EventSystem.current.IsPointerOverGameObject())
			return;
		if (_raycastCamera == null)
		{
			_raycastCamera = Camera.main;
		}
		if (_raycastCamera == null)
			return;
		
		Ray mRay = _raycastCamera.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(mRay, out RaycastHit hitInfo, 100, interactable))
		{
			if (hitInfo.transform.TryGetComponent(out ItemInteraction item))
			{
				if (item.Interactable)
				{
					if (item != _hoveredItem.Value)
					{
						if (_hoveredItem.Value != null)
						{
							_hoveredItem.Value.outline.OutlineWidth = 0;
						}

						_hoveredItem.Value = item;
					}

					_hoveredItem.Value.outline.OutlineWidth = itemOutlineWidth;

					if (Input.GetMouseButtonDown(0) && _currentAction == _idleAction)
					{
						InteractWith(item);
					} 
					else if (Input.GetMouseButtonDown(1))
					{
						if (VariableSystem.Instance != null)
						{
							var info = _hoveredItem.Value.GetOverviewInfo(VariableSystem.Instance);
#if UNITY_EDITOR
							if (string.IsNullOrEmpty(info))
							{
								Debug.LogWarning($"[ItemOverview] Overview not set on {_hoveredItem.Value.gameObject.name}");
								UnityEditor.Selection.activeGameObject = _hoveredItem.Value.gameObject;
							}
#endif
							VariableSystem.Instance.ItemOverview.ShowOverview(info);
						}
						
						var direction = item.transform.position - transform.position;
						direction.y = 0f;
						_playerActions.Enqueue(new RotateToTarget(this, direction.normalized));
					}
				}
				else
					UnHoverItem();
			}
			else
				UnHoverItem();
		}
		else
			UnHoverItem();
	}

	private void UnHoverItem()
	{
		if (_hoveredItem.Value == null)
			return;
		_hoveredItem.Value.outline.OutlineWidth = 0;
		_hoveredItem.Set(null);
	}

	private void UpdateActions()
	{
		if (_currentAction == _idleAction || _currentAction.Completed() || _currentAction.Failed)
		{
			if (_playerActions.Count > 0)
			{
				_currentAction = _playerActions.Dequeue();
			}
			else
			{
				_currentAction = _idleAction;
			}
		}

		var success = _currentAction.Update();
		if (!success)
		{
			_playerActions.Clear();
		}
		_playerState.SetOnce(_currentAction.State);
	}

	public string GetCurrentAction()
	{
		return _currentAction.ToString();
	}

	private void InteractWith(ItemInteraction item)
	{
		switch (item.interactionInfo.interactionMode)
		{
			case EInteractionMode.Instant:
			{
				var direction = item.transform.position - transform.position;
				direction.y = 0f;
				_playerActions.Enqueue(new RotateToTarget(this, direction.normalized));
			}
				break;

			case EInteractionMode.Radius:
			{
				var target = item.transform.position;
				target.y = 0;
				if (Vector3.Distance(transform.position, target) > 0.3f)
				{
					_playerActions.Enqueue(new MoveToPoint(this, target));
				}
			}
				break;

			case EInteractionMode.FixedPoint:
			{
				var spot = item.interactionInfo.interactionSpot;
				if (Vector3.Distance(transform.position, spot.position) > 0.3)
				{
					_playerActions.Enqueue(new MoveToPoint(this, spot.position));
				}
				_playerActions.Enqueue(new RotateToTarget(this, spot.forward));
			}
				break;
		}

		var interactAction = GetAction(item);
		if (interactAction != null)
		{
			_playerActions.Enqueue(interactAction);
		}
	}

	private IPlayerAction GetAction(ItemInteraction interaction)
	{
		switch (interaction.ProvideState)
		{
			case PlayerState.TakeItem:
				return new TakeItem(VariableSystem.Instance, this, interaction);
			case PlayerState.Interact:
				return new Interact(VariableSystem.Instance, interaction);
			case PlayerState.IdleInteract:
				return new IdleInteract(VariableSystem.Instance, interaction);
			case PlayerState.UseItem:
				return new UseItem(VariableSystem.Instance, interaction);

		}

		return null;
	}
}
using System.Collections.Generic;
using Player.Commands;
using Unity.AI.Navigation;
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
	}

	public float itemOutlineWidth;

	public IObservableVar<PlayerState> State => _playerState;
	[SerializeField] private LayerMask groundLayer;
	[SerializeField] private LayerMask interactable;


	public IObservableVar<ItemInteraction> ActiveItem => _activeItem;
	public IObservableVar<ItemInteraction> HoveredItem => _hoveredItem;
	public string currentAction;

	//public UIItemInfo itemInfo;

	private readonly ObservableVar<ItemInteraction> _activeItem = new ObservableVar<ItemInteraction>();
	private readonly ObservableVar<ItemInteraction> _hoveredItem = new ObservableVar<ItemInteraction>();

	private PlayerState _previousState;
	private NavMeshAgent _navMeshAgent;
	private IPlayerAction _currentAction;
	private readonly Queue<IPlayerAction> _playerActions = new Queue<IPlayerAction>();
	private readonly Idle _idleAction = new Idle();
	private Camera _raycastCamera;

	private readonly ObservableVar<PlayerState> _playerState = new ObservableVar<PlayerState>();

	
	private string _sceneName;


	private void Awake()
	{
		_navMeshAgent = GetComponent<NavMeshAgent>();
	}

	private void Start()
	{
		_raycastCamera = Camera.main;
		_currentAction = _idleAction;
		ActiveItem.OnValueChanged += OnSelectItem;
		_sceneName = SceneManager.GetActiveScene().name;
	}
	
	private void OnDestroy()
	{
		ActiveItem.OnValueChanged -= OnSelectItem;
	}

	private void OnSelectItem(ItemInteraction previous, ItemInteraction item)
	{
		InteractWith(item);
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
		
		if (State.Value != PlayerState.Idle && State.Value != PlayerState.Run) return;
		if (Input.GetMouseButtonDown(0) && _hoveredItem.Value == null)
		{
			Ray mRay = _raycastCamera.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(mRay.origin, mRay.direction, out RaycastHit hitInfo, 100, groundLayer))
			{
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

					if (Input.GetMouseButtonDown(0))
					{
						_activeItem.Set(item);
					}
				}
				else
				{
					UnHoverItem();	
				}
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
		if (_currentAction == _idleAction || _currentAction.Completed())
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

		Debug.Assert(_currentAction != null, "[Player] Current Action is null!");

		_currentAction.Update();
		currentAction = _currentAction.ToString();
		_playerState.SetOnce(_currentAction.State);
	}

	public void InteractWith(ItemInteraction item)
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
				_playerActions.Enqueue(new MoveToPoint(this, target));
			}
				break;

			case EInteractionMode.FixedPoint:
			{
				var spot = item.interactionInfo.interactionSpot;
				var position = spot.position;
				_playerActions.Enqueue(new MoveToPoint(this, position));
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
				return new Interact(VariableSystem.Instance, this, interaction);
		}

		return null;
	}
}
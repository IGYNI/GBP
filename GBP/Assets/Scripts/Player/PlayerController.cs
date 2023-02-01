using System.Collections.Generic;
using FMOD.Studio;
using Player.Commands;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.AI;

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
	

	public IObservableVar<Item> ActiveItem => _activeItem;
	public IObservableVar<Item> HoveredItem => _hoveredItem;
	public string currentAction;
	
	//public UIItemInfo itemInfo;
	
	private readonly ObservableVar<Item> _activeItem = new ObservableVar<Item>();
	private readonly ObservableVar<Item> _hoveredItem = new ObservableVar<Item>();

	private PlayerState _previousState;
	private NavMeshAgent _navMeshAgent;
	private IPlayerAction _currentAction;
	private readonly Queue<IPlayerAction> _playerActions = new Queue<IPlayerAction>();
	private readonly Idle _idleAction = new Idle();
	private Camera _raycastCamera;

	private readonly ObservableVar<PlayerState> _playerState = new ObservableVar<PlayerState>();

    private EventInstance footsteps;

    private void Awake()
	{
		_navMeshAgent = GetComponent<NavMeshAgent>();
	}

	private void Start()
	{
		_raycastCamera = Camera.main;
		_currentAction = _idleAction;
		ActiveItem.OnValueChanged += OnSelectItem;
        footsteps = AudioManager.instance.CreateInstance(FMODEvents.instance.footsteps);
    }

	private void OnSelectItem(Item previous, Item item)
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
        UpdateSounds();

    }

	private void UpdateNavigation()
	{
		if (State.Value == PlayerState.Idle || State.Value == PlayerState.Run)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Ray mRay = _raycastCamera.ScreenPointToRay(Input.mousePosition); 

				if (Physics.Raycast(mRay.origin, mRay.direction, out RaycastHit hitInfo, 100, groundLayer))
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
		Ray mRay = _raycastCamera.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(mRay, out RaycastHit hitInfo, 100,interactable))
		{
			if (hitInfo.transform.TryGetComponent(out Item item))
			{
				if (item != _hoveredItem.Value)
				{
					if (_hoveredItem.Value != null)
					{
						_hoveredItem.Value.itemOutline.OutlineWidth = 0;
					}
					_hoveredItem.Value = item;
				}

				_hoveredItem.Value.itemOutline.OutlineWidth = itemOutlineWidth;

				if (Input.GetMouseButtonDown(0))
				{
					_activeItem.Set(item);
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
		_hoveredItem.Value.itemOutline.OutlineWidth = 0;
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

	public void InteractWith(Item item)
	{
		switch (item.interactionInfo.interactionMode)
		{
			case EInteractionMode.Instant:
			{
				var direction = item.transform.position - transform.position;
				direction.y = 0f;
				_playerActions.Enqueue(new RotateToTarget(this, direction.normalized));
				// if (item is IInteractable interactable)
				// {
				// 	_playerActions.Enqueue(new Interact(this, interactable));
				// }
			}
				break;
			case EInteractionMode.Radius:
			{
				var target = item.transform.position;
				target.y = 0;
				_playerActions.Enqueue(new MoveToPoint(this, target));
				break;
			}
			case EInteractionMode.FixedPoint:
			{
				var spot = item.interactionInfo.interactionSpot;
				var position = spot.position;
				position.y = 0;
				_playerActions.Enqueue(new MoveToPoint(this, position));
				_playerActions.Enqueue(new RotateToTarget(this, spot.forward));
				break;
			}
		}
	}
    private void UpdateSounds()
    {
		PLAYBACK_STATE playbackstate;
        footsteps.getPlaybackState(out playbackstate);
		if (State.Value == PlayerState.Run && playbackstate.Equals(PLAYBACK_STATE.STOPPED))
			{
			footsteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
			footsteps.start();
		}
		}
}

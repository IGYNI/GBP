using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
	public float itemOutlineWidth;

	public IObservableVar<Item> ActiveItem => _activeItem;
	public IObservableVar<Item> HoveredItem => _hoveredItem;
	
	//public UIItemInfo itemInfo;
	
	private readonly ObservableVar<Item> _activeItem = new ObservableVar<Item>();
	private readonly ObservableVar<Item> _hoveredItem = new ObservableVar<Item>();
	
	private Camera _mainCamera;
	private void Start()
	{
		_mainCamera = Camera.main;
	}

	private void Update()
	{
		Ray mRay = _mainCamera.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(mRay, out RaycastHit hitInfo))
		{
			var item = hitInfo.transform.GetComponent<Item>();
			if (item != null)
			{
				if (item != _hoveredItem.Value)
				{
					if (_hoveredItem != null)
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
			else if (_hoveredItem != null)
			{
				_hoveredItem.Value.itemOutline.OutlineWidth = 0;
				_hoveredItem.Set(null);
			}
		}
	}
}
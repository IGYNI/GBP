using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] private Image icon;
	public bool isFull;
	public ItemInfo ItemInfo => _itemInfo;
	
	private ItemInfo _itemInfo;

	public IItemHandler ItemHandler { get; set; }

	public void SetItem(ItemInfo itemInfo)
	{
		_itemInfo = itemInfo;
		if (itemInfo != null)
		{
			isFull = true;
			icon.sprite = itemInfo.icon;
			icon.enabled = true;
		}
		else
		{
			isFull = false;
			icon.enabled = false;
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (ItemHandler != null && _itemInfo != null)
		{
			ItemHandler.ProcessItem(_itemInfo);
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (_itemInfo != null)
		{
			if (VariableSystem.Instance != null)
			{
				VariableSystem.Instance.ItemOverview.SetItemInfo(_itemInfo);
			}
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (VariableSystem.Instance != null)
		{
			VariableSystem.Instance.ItemOverview.SetItemInfo((ItemInfo)null);
		}
	}
}
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
	[SerializeField] private Image icon;
	public bool isFull;
	public ItemInfo ItemInfo => _itemInfo;
	
	private ItemInfo _itemInfo;

	public void SetItem(ItemInfo itemInfo)
	{
		_itemInfo = itemInfo;
		if (itemInfo != null)
		{
			icon.sprite = itemInfo.icon;
			icon.enabled = true;
		}
		else
		{
			icon.enabled = false;
		}
	}
}
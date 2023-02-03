using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, ItemHandler
{
    [SerializeField] private List<ItemSlot> slots;
    [SerializeField] private List<ItemInfo> itemDatabase;

    public void SetItemHandler(ItemHandler itemHandler)
    {
        foreach (ItemSlot slot in slots)
        {
            slot.ItemHandler = itemHandler;
        }
    }
    
    public void AddItem(string itemName)
    {
        var info = GetItemInfo(itemName);
        if (info != null)
        {
            AddItem(info);
        }
    }

    public void AddItem(ItemInfo item)
    {
        foreach (ItemSlot slot in slots)
        {
            if (slot.isFull) 
                continue;
            slot.isFull = true;
            slot.SetItem(item);
            break;
        }
    }

    public void RemoveItem(ItemInfo itemInfo)
    {
        foreach (ItemSlot slot in slots)
        {
            if (slot.isFull && slot.ItemInfo == itemInfo)
            {
                slot.isFull = false;
                slot.SetItem(null);
                break;
            }
        }
    }
    
    public void RemoveItem(string itemName)
    {
        foreach (ItemSlot slot in slots)
        {
            if (slot.isFull && slot.ItemInfo.itemName == itemName)
            {
                slot.isFull = false;
                slot.SetItem(null);
                break;
            }
        }
    }

    private ItemInfo GetItemInfo(string itemName)
    {
        foreach (ItemInfo itemInfo in itemDatabase)
        {
            if (itemInfo.itemName == itemName)
                return itemInfo;
        }

        return null;
    }

    public void ProcessItem(ItemInfo itemInfo)
    {
        AddItem(itemInfo);
    }
}

using System.Collections.Generic;
using UnityEngine;

public interface IItemHandler
{
    void ProcessItem(ItemInfo itemInfo);
}


public class Inventory : MonoBehaviour
{
    [SerializeField] private List<ItemSlot> slots;
    [SerializeField] private List<ItemInfo> itemDatabase;
    private VariableSystem _variableSystem;

    public void Init(VariableSystem variableSystem)
    {
        _variableSystem = variableSystem;
    }
    
    public void SetItemHandler(IItemHandler itemHandler)
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

    public void AddItem(ItemInfo itemInfo)
    {
        _variableSystem.SetVariable(itemInfo.itemName + Item.TakenSuffix, "true", true);
        foreach (ItemSlot slot in slots)
        {
            if (slot.isFull) 
                continue;
            slot.isFull = true;
            slot.SetItem(itemInfo);
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
                _variableSystem.SetVariable(itemInfo.itemName + Item.TakenSuffix, "false", true);
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
}

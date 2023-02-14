using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public interface IItemHandler
{
    void ProcessItem(ItemInfo itemInfo);
}


public class Inventory : MonoBehaviour
{
    [SerializeField] private List<ItemSlot> slots;
    [SerializeField] private List<ItemInfo> itemDatabase;
    [SerializeField] private RectTransform newItemView;
    [SerializeField] private RectTransform showAnchor;
    [SerializeField] private RectTransform hideAnchor;
    [SerializeField] private Image newItemIcon;
    [SerializeField] private float showTime;
    [SerializeField] private float animationTime;
    private readonly Queue<ItemInfo> _showQueue = new Queue<ItemInfo>();
    private bool _newItemShow;

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

    private void Update()
    {
        if (!_newItemShow)
        {
            if (_showQueue.Count > 0)
            {
                _newItemShow = true;
                var newItem = _showQueue.Dequeue();
                newItemIcon.sprite = newItem.icon;
                StartCoroutine(ShowNewItemCor());
            }
        }
    }

    private IEnumerator ShowNewItemCor()
    {
        var showTween = newItemView.DOMove(showAnchor.position, animationTime).SetEase(Ease.OutSine);
        yield return showTween.WaitForCompletion();
        yield return new WaitForSeconds(showTime);
        var hideTween = newItemView.DOMove(hideAnchor.position, animationTime).SetEase(Ease.InSine);
        yield return hideTween.WaitForCompletion();
        _newItemShow = false;
    }

    // public void AddItem(string itemName)
    // {
    //     var info = GetItemInfo(itemName);
    //     if (info != null)
    //     {
    //         AddItem(info);
    //     }
    // }

    public void AddItem(ItemInfo itemInfo)
    {
        _variableSystem.SetVariable(itemInfo.itemName + Item.TakenSuffix, "true", true);
        foreach (ItemSlot slot in slots)
        {
            if (slot.isFull) 
                continue;
            slot.isFull = true;
            slot.SetItem(itemInfo);
            _showQueue.Enqueue(itemInfo);
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
    
    // public void RemoveItem(string itemName)
    // {
    //     foreach (ItemSlot slot in slots)
    //     {
    //         if (slot.isFull && slot.ItemInfo.itemName == itemName)
    //         {
    //             slot.isFull = false;
    //             slot.SetItem(null);
    //             break;
    //         }
    //     }
    // }

    // private ItemInfo GetItemInfo(string itemName)
    // {
    //     foreach (ItemInfo itemInfo in itemDatabase)
    //     {
    //         if (itemInfo.itemName == itemName)
    //             return itemInfo;
    //     }
    //
    //     return null;
    // }
}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class UseItemInteraction : ItemInteraction
{
	public override PlayerController.PlayerState ProvideState
	{
		get
		{
			if (condition.Satisfied())
				return PlayerController.PlayerState.Interact;
			return PlayerController.PlayerState.IdleInteract;
		}
	}

	public override bool Interactable { get; protected set; }
	
	[SerializeField] private CheckCondition condition;
	[SerializeField] private string overview;
	[SerializeField] private string failOverview;
	[SerializeField] private ItemInfo usedItem;
	[SerializeField] private GameObject itemView;
	[SerializeField] private GameObject playerMountPoint;
	[SerializeField] private bool removeAfterUse;
	[SerializeField] private ItemInfo returnItem;
	[SerializeField] private UnityEvent onUseItem;

	private void Awake()
	{
		Interactable = true;
	}

	public override void Interact(VariableSystem variableSystem)
	{
		if (condition.Satisfied())
		{
			variableSystem.SetVariable(usedItem.itemName+"_ON_"+item.info.itemName + Item.UsedSuffix, "true", true);
			if (removeAfterUse)
			{
				variableSystem.Inventory.RemoveItem(usedItem);
				variableSystem.SetVariable(usedItem.itemName + Item.TakenSuffix, "false");
			}

			if (returnItem != null)
			{
				variableSystem.SetVariable(returnItem.itemName + Item.TakenSuffix, "true", true);
				variableSystem.Inventory.AddItem(returnItem);
			}

			StartCoroutine(UseItemCor());
		}
	}

	private IEnumerator UseItemCor()
	{
		Interactable = false;
		yield return new WaitForSeconds(0.3f);
		if (itemView != null && playerMountPoint != null)
		{
			itemView.transform.SetParent(playerMountPoint.transform);
			itemView.transform.localPosition = Vector3.zero;
			itemView.transform.localRotation = Quaternion.identity;
			itemView.SetActive(true);
			yield return new WaitForSeconds(0.5f);
			itemView.transform.SetParent(null);
			itemView.SetActive(false);
		}
		else
		{
			yield return new WaitForSeconds(0.5f);
		}
		onInteract.Invoke();
		onUseItem.Invoke();
	}

	public override void LoadState(VariableSystem variableSystem)
	{
		var variable = variableSystem.GetVariable(usedItem.itemName+"_ON_"+item.info.itemName + Item.UsedSuffix);
		if (variable != null && variable.Value == "true")
		{
			onUseItem.Invoke();
			Interactable = false;
		}
	}

	public override string GetOverviewInfo(VariableSystem variableSystem)
	{
		if (condition != null)
		{
			return condition.Satisfied() ? overview : failOverview;
		}
		return overview;
	}
}

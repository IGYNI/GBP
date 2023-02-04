using UnityEngine;

public class SearchItemInteraction : ItemInteraction
{
	public override PlayerController.PlayerState ProvideState => PlayerController.PlayerState.TakeItem;
	public override bool Interactable { get; protected set; }
	
	[SerializeField] private string overviewInfo;
	[SerializeField] private ItemInfo hiddenItem;
	[SerializeField] private NoteInfo noteInfo;

	private void Awake()
	{
		Interactable = true;
	}

	public override void Interact(VariableSystem variableSystem)
	{
		variableSystem.SetVariable(item.info.itemName + Item.ExploredSuffix, "true", true);
		if (hiddenItem != null)
		{
			variableSystem.SetVariable(hiddenItem.itemName + Item.TakenSuffix, "true", true);
			variableSystem.Inventory.AddItem(hiddenItem);
        }

        if (noteInfo != null)
		{
			variableSystem.SetVariable(noteInfo.noteName + Item.TakenSuffix, "true", true);
			variableSystem.NoteBook.Add(noteInfo);
		}

		Interactable = false;
	}

	public override void LoadState(VariableSystem variableSystem)
	{
		GameVar variable = variableSystem.GetVariable(item.info.itemName + Item.ExploredSuffix);
		if (variable != null)
		{
			Interactable = variable.Value != "true";
		}

		Debug.Log($"[Interaction] Item {item.info.itemName} interactable: {Interactable.ToString()}");
	}

	public override string GetOverviewInfo(VariableSystem variableSystem)
	{
		return overviewInfo;
	}
}

using UnityEngine;

namespace Items
{
	public class TakeItemInteraction : ItemInteraction
	{
		public override PlayerController.PlayerState ProvideState => PlayerController.PlayerState.TakeItem;
		public override bool Interactable { get; protected set; }

		[SerializeField] private string overviewInfo;

		private void Awake()
		{
			Interactable = true;
		}

		public override void Interact(VariableSystem variableSystem)
		{
			variableSystem.SetVariable(item.info.itemName + Item.TakenSuffix, "true", true);
			variableSystem.SetVariable(item.info.itemName + Item.VisibleSuffix, "false", true);
			variableSystem.Inventory.AddItem(item.info.itemName);
			Destroy(gameObject);		
		}

		public override void LoadState(VariableSystem variableSystem)
		{
		}

		public override string GetOverviewInfo(VariableSystem variableSystem)
		{
			return overviewInfo;
		}
	}
}
using UnityEngine;

namespace Items.Interaction
{
	public class AnalyzerInteraction : ItemInteraction
	{
		public override PlayerController.PlayerState ProvideState =>
			condition.Satisfied()
				? PlayerController.PlayerState.Interact
				: PlayerController.PlayerState.IdleInteract;

		public override bool Interactable { get; protected set; }
		[SerializeField] private CheckCondition condition;
		[SerializeField] private string failConditionOverview;
		[SerializeField] private string overview;

		[SerializeField] private ItemInfo analyzedItem;

		[SerializeField] private ItemInfo hiddenItem;
		[SerializeField] private NoteInfo noteInfo;

		private PlayerController.PlayerState _state;

		private void Awake()
		{
			Interactable = true;
		}

		public override void Interact(VariableSystem variableSystem)
		{
			if (condition.Satisfied())
			{
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

				variableSystem.SetVariable(analyzedItem.itemName + Item.AnalyzedSuffix, "true", true);
				onInteract.Invoke();
			}
		}

		public override void LoadState(VariableSystem variableSystem)
		{
		}

		public override string GetOverviewInfo(VariableSystem variableSystem)
		{
			if (!condition.Satisfied())
			{
				return failConditionOverview;
			}

			return overview;
		}
	}
}
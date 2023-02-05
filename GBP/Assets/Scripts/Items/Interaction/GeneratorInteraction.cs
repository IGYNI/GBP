using System.Collections;
using UnityEngine;

public class GeneratorInteraction : ItemInteraction
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
	[SerializeField] private string overview;
	[SerializeField] private string failOverview;
	[SerializeField] private NoteInfo noteInfo;
	[SerializeField] private ItemInfo consumeItem;
	[SerializeField] private GeneratorPuzzle generator;
	[SerializeField] private CheckCondition condition;
	[SerializeField] private CircuitStateView сircuitStateView;
	

	private VariableSystem _variableSystem;

	private void Awake()
	{
		Interactable = true;
		generator.State.OnValueChanged += HandleGeneratorState;
	}

	private void HandleGeneratorState(Puzzle.EState prev, Puzzle.EState current)
	{
		if (current == Puzzle.EState.Complete)
		{
			_variableSystem.SetVariable(item.info.itemName + Item.UnlockedSuffix, "true", true);

			if (noteInfo != null)
			{
				_variableSystem.SetVariable(noteInfo.noteName + Item.TakenSuffix, "true", true);
				_variableSystem.NoteBook.Add(noteInfo);
			}

			if (consumeItem != null)
			{
				_variableSystem.SetVariable(consumeItem.itemName + Item.TakenSuffix, "false", true);
				_variableSystem.Inventory.RemoveItem(consumeItem);
			}
			
			сircuitStateView.ActiveState();
			Interactable = false;
		}
	}

	public override void Interact(VariableSystem variableSystem)
	{
		_variableSystem = variableSystem;
		if (condition.Satisfied())
		{
			StartCoroutine(LaunchPuzzle());
		}
	}

	private IEnumerator LaunchPuzzle()
	{
		onInteract.Invoke();
		yield return new WaitForSeconds(0.5f);
		generator.InitPuzzle();
		generator.Show();
	}

	public override void LoadState(VariableSystem variableSystem)
	{
		GameVar variable = variableSystem.GetVariable(item.info.itemName + Item.UnlockedSuffix);
		if (variable != null)
		{
			Interactable = variable.Value != "true";
			сircuitStateView.ActiveState();
			generator.SetFullPower();
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
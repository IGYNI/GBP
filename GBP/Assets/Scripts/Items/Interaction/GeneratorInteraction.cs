using System.Collections;
using DG.Tweening;
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
	[SerializeField] private InteractionSequence sequence;
	[SerializeField] private CircuitStateView сircuitStateView;
	[Header("Battery view")]
	[SerializeField] private GameObject view;
	[SerializeField] private Transform origin;
	[SerializeField] private Transform target;
	[SerializeField] private float sequenceTime = 1f;
	

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
		//yield return InsertBattery();
		//generator.InitPuzzle();
		//generator.Show();
		if (sequence != null)
		{
			yield return sequence.Proceed();

		}
		else
		{
			yield return new WaitForSeconds(0.5f);
		}
		generator.InitPuzzle();
		generator.Show();
	}

	private IEnumerator InsertBattery()
	{
		view.transform.position = origin.transform.position;
		view.transform.rotation = origin.transform.rotation;
		view.SetActive(true);
		view.transform.DORotate(target.transform.eulerAngles, sequenceTime);
		var tween = view.transform.DOMove(target.transform.position, sequenceTime).SetEase(Ease.Linear);
		yield return tween.WaitForCompletion();
		view.SetActive(false);
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
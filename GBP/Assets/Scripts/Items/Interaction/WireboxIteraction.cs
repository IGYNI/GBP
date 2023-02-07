using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WireboxIteraction : ItemInteraction
{
	[SerializeField] private string overview;
	[SerializeField] private WireBoxPuzzle puzzle;
	[SerializeField] private UnityEvent onStartPuzzle;
	private VariableSystem _variableSystem;

	public override PlayerController.PlayerState ProvideState => PlayerController.PlayerState.Interact;
	public override bool Interactable { get; protected set; }

	private void Awake()
	{
		Interactable = true;
		puzzle.State.OnValueChanged += HandlePuzzleState;
	}

	private void HandlePuzzleState(Puzzle.EState prev, Puzzle.EState current)
	{
		if (current == Puzzle.EState.Complete)
		{
			if (_variableSystem != null)
			{
				_variableSystem.SetVariable(item.info.itemName + Item.UnlockedSuffix, "true", true);
			}
			Interactable = false;
		}
	}

	public override void Interact(VariableSystem variableSystem)
	{
		_variableSystem = variableSystem;
		StartCoroutine(InteractCor());
	}

	private IEnumerator InteractCor()
	{
		onStartPuzzle.Invoke();
		yield return new WaitForSeconds(1f);
		puzzle.Show();
	}

	public override void LoadState(VariableSystem variableSystem)
	{
		var variable = variableSystem.GetVariable(item.info.itemName + Item.UnlockedSuffix);
		if (variable != null)
		{
			Interactable = variable.Value != "true";
		}
	}

	public override string GetOverviewInfo(VariableSystem variableSystem)
	{
		return overview;
	}
}

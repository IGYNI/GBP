using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerTerminalInteraction : ItemInteraction
{
	[Serializable]
	public class ConditionInfo
	{
		public string overview;
		public BasicCondition condition;
	}

	private void Awake()
	{
		Interactable = true;
	}

	public override PlayerController.PlayerState ProvideState {
		get
		{
			if (AllSatisfied()) return PlayerController.PlayerState.Interact;
			return PlayerController.PlayerState.Interact;
		}
	}
	public override bool Interactable { get; protected set; }

	[SerializeField] private List<ConditionInfo> conditions;
	[SerializeField] private string overviewInfo;

	private bool AllSatisfied()
	{
		var satisfied = true;
		foreach (ConditionInfo conditionInfo in conditions)
		{
			if (!conditionInfo.condition.Satisfied())
				satisfied = false;
		}

		return satisfied;
	}

	public override void Interact(VariableSystem variableSystem)
	{
		if (AllSatisfied())
		{
			StartCoroutine(UseTerminal());
		}
	}

	private IEnumerator UseTerminal()
	{
		yield return new WaitForSeconds(0.6f);
		onInteract.Invoke();
	}

	public override void LoadState(VariableSystem variableSystem)
	{
		
	}

	public override string GetOverviewInfo(VariableSystem variableSystem)
	{
		foreach (var conditionInfo in conditions)
		{
			if (!conditionInfo.condition.Satisfied())
			{
				return conditionInfo.overview;
			}
		}

		return overviewInfo;
	}
}

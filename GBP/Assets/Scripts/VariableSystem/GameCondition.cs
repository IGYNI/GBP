using System;
using System.Collections.Generic;
using UnityEngine;

public class GameCondition : MonoBehaviour
{
	public VariableSystem variableSystem;
	public List<VariableDesc> variableConditions;
	public List<GameAction> actions;

	public float repeat = 0.2f;
	private float timer;

	private void Update()
	{
		if (timer >= repeat)
		{
			timer = 0f;
			CheckConditions();
		}

		timer += Time.deltaTime;
	}

	private void CheckConditions()
	{
		bool satisfied = true;
		foreach (VariableDesc variableDesc in variableConditions)
		{
			var gameVar = variableSystem.GetVariable(variableDesc.name);
			if (gameVar == null) continue;
			if (gameVar.Value != variableDesc.value)
			{
				satisfied = false;
				break;
			}
		}

		if (!satisfied) 
			return;
		foreach (GameAction gameAction in actions)
		{
			gameAction.Invoke(variableSystem);
		}
	}
}

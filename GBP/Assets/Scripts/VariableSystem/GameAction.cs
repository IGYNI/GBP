using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class GameAction
{
	public UnityEvent unityEvents;
	public List<GameVarEvent> gameVarEvents;

	public void Invoke(GameObject sender, VariableSystem variableSystem)
	{
		unityEvents.Invoke();

		foreach (var gameVarEvent in gameVarEvents)
		{
			var result = variableSystem.SetVariable(gameVarEvent.variableName, gameVarEvent.newValue);
			if (!result)
				Debug.LogWarning($@"[GameVar] Can't set variable {{gameVarEvent.variableName}} on {sender.name}");
		}
	}
	
}

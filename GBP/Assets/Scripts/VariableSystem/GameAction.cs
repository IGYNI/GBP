using System;
using System.Collections.Generic;
using UnityEngine.Events;

[Serializable]
public class GameAction
{
	public UnityEvent unityEvents;
	public List<GameVarEvent> gameVarEvents;

	public void Invoke(VariableSystem variableSystem)
	{
		unityEvents.Invoke();

		foreach (var gameVarEvent in gameVarEvents)
		{
			variableSystem.SetVariable(gameVarEvent.variableName, gameVarEvent.newValue);
		}
	}
	
}

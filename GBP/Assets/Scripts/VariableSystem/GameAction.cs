using System;
using System.Collections.Generic;
using UnityEngine.Events;

[Serializable]
public class GameAction
{
	public List<UnityEvent> unityEvents;
	public List<GameVarEvent> gameVarEvents;

	public void Invoke(VariableSystem variableSystem)
	{
		foreach (var unityEvent in unityEvents)
		{
			unityEvent.Invoke();
		}

		foreach (var gameVarEvent in gameVarEvents)
		{
			variableSystem.SetVariable(gameVarEvent.variableName, gameVarEvent.newValue);
		}
	}
	
}

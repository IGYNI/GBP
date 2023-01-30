using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableSystem : MonoBehaviour
{
    public List<VariableDesc> initialVariables;

    public IReadOnlyDictionary<string, GameVar> Variables => _gameVariables; 

    private readonly Dictionary<string, GameVar> _gameVariables = new Dictionary<string, GameVar>();

    private void Awake()
    {
        foreach (var variable in initialVariables)
        {
            var gameVar = new GameVar(variable.name, variable.value);
            _gameVariables.Add(variable.name, gameVar);
        }
    }

    public GameVar GetVariable(string variableName)
    {
        if (_gameVariables.TryGetValue(variableName, out var gameVar))
        {
            return gameVar;
        }

        return null;
    }
    
    public void SetVariable(string variableName, string newValue)
    {
        if (string.IsNullOrEmpty(variableName))
            return;
        
        if (_gameVariables.TryGetValue(variableName, out var gameVar))
        {
            gameVar.Value = newValue;
        }
    }
    
    public void CreateVariable(string variableName, string initialValue)
    {
        if (string.IsNullOrEmpty(variableName))
            return;
        
        var gameVar = new GameVar(variableName, initialValue);
        if (!_gameVariables.TryAdd(variableName, gameVar))
        {
            Debug.LogWarning($"[GameVar] Variable {variableName} already exists");
        }
    }
}

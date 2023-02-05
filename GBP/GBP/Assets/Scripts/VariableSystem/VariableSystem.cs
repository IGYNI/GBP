using System;
using System.Collections.Generic;
using UnityEngine;

public class VariableSystem : MonoBehaviour
{
    public static VariableSystem Instance { get; private set; }

    public event Action<GameVar> OnCreateVariable;
    public Inventory Inventory;
    public NoteBook NoteBook;
    [field: SerializeField] public ItemOverview ItemOverview { get; private set; }

    public List<VariableDesc> initialVariables;

    public IReadOnlyDictionary<string, GameVar> Variables => _gameVariables; 

    private readonly Dictionary<string, GameVar> _gameVariables = new Dictionary<string, GameVar>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializeSystem();
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void InitializeSystem()
    {
        foreach (var variable in initialVariables)
        {
            var gameVar = new GameVar(variable.name, variable.value);
            _gameVariables.Add(variable.name, gameVar);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public GameVar GetVariable(string variableName)
    {
        return _gameVariables.TryGetValue(variableName, out GameVar gameVar) ? gameVar : null;
    }
    
    public bool SetVariable(string variableName, string newValue, bool createVariable = false)
    {
        if (string.IsNullOrEmpty(variableName))
            return false;
        
        if (_gameVariables.TryGetValue(variableName, out GameVar gameVar))
        {
            gameVar.Value = newValue;
            return true;
        }
        if (createVariable)
        {
            CreateVariable(variableName, newValue);
            return true;
        }
        Debug.LogWarning($"[GameVar] Variable {variableName} doesn't exists");
        return false;
    }
    
    public void CreateVariable(string variableName, string initialValue)
    {
        if (string.IsNullOrEmpty(variableName))
            return;
        
        var gameVar = new GameVar(variableName, initialValue);
        if (_gameVariables.TryAdd(variableName, gameVar))
        {
            OnCreateVariable?.Invoke(gameVar);
        }
        else
        {
            Debug.LogWarning($"[GameVar] Variable {variableName} already exists");
        }
    }
}

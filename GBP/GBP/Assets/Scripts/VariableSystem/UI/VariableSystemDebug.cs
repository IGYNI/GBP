using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableSystemDebug : MonoBehaviour
{
    [SerializeField] private UIVariableDesc variableDescTemplate;

    [SerializeField] private VariableSystem variableSystem;
    [SerializeField] private Transform variablesRoot;
    

    private void Awake()
    {
        variableSystem.OnCreateVariable += CreateDebugSlot;
    }

    private void CreateDebugSlot(GameVar gameVar)
    {
        var view = Instantiate(variableDescTemplate, variablesRoot);
        view.Init(gameVar);
    }

    private void Start()
    {
        foreach (var gameVar in variableSystem.Variables)
        {
            CreateDebugSlot(gameVar.Value);
        }
    }
}

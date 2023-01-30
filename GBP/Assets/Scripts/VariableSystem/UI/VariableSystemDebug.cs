using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableSystemDebug : MonoBehaviour
{
    [SerializeField] private UIVariableDesc variableDescTemplate;

    [SerializeField] private VariableSystem variableSystem;
    [SerializeField] private Transform variablesRoot;
    // Start is called before the first frame update
    private void Start()
    {
        foreach (var gameVar in variableSystem.Variables)
        {
            var view = Instantiate(variableDescTemplate, variablesRoot);
            view.Init(gameVar.Value);
        }
    }
}

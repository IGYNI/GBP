using TMPro;
using UnityEngine;

public class VariableSystemDebug : MonoBehaviour
{
    [SerializeField] private UIVariableDesc variableDescTemplate;

    [SerializeField] private VariableSystem variableSystem;
    [SerializeField] private Transform variablesRoot;
    [SerializeField] private TMP_Text hover;
    [SerializeField] private TMP_Text actions;
    [SerializeField] private TMP_Text currentAction;
    
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

    private void Update()
    {
        var player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            if (player.HoveredItem.Value != null)
                hover.text = player.HoveredItem.Value.item.gameObject.name;
            else
                hover.text = "N/A";
            
            // if (player.ActiveItem.Value != null)
            //     hover.text = player.ActiveItem.Value.item.gameObject.name;
            // else
            //     active.text = "N/A";
            actions.text = $"Total: {player.Actions.Count.ToString()}";
            currentAction.text = player.GetCurrentAction();
        }
        else
        {
            hover.text = "N/A";
            actions.text = "N/A";
            currentAction.text = "N/A";
        }
    }
}

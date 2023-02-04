using UnityEngine;

public class MoveItemInteraction : ItemInteraction
{
    [SerializeField] private string overviewInfo;
    [SerializeField] private Transform originPoint;
    [SerializeField] private Transform targetPoint;
    [SerializeField] private Transform view;
    [SerializeField] private GameAction OnInteract;

    private bool _started;
    private bool _finished;

    private float _timer;
    
    public override PlayerController.PlayerState ProvideState => PlayerController.PlayerState.Interact;
    public override bool Interactable { get; protected set; }

    private void Awake()
    {
        Interactable = true;
    }

    public override void Interact(VariableSystem variableSystem)
    {
        _started = true;
        variableSystem.SetVariable(item.info.itemName + Item.MovedSuffix, "true", true);
        OnInteract.Invoke(item.gameObject, variableSystem);
    }

    private void Update()
    {
        if (_started && !_finished)
        {
            var t = Mathf.Clamp01(_timer / InteractionTime);
            view.position = Vector3.Lerp(originPoint.position, targetPoint.position, t);
            view.rotation = Quaternion.Lerp(originPoint.rotation, targetPoint.rotation, t);
            _timer += Time.deltaTime;
            _finished = _timer >= InteractionTime;
        }
    }

    public override void LoadState(VariableSystem variableSystem)
    {
        GameVar variable = variableSystem.GetVariable(item.info.itemName + Item.MovedSuffix);
        if (variable != null)
        {
            Interactable = variable.Value != "true";
        }

        view.position = Interactable ? originPoint.position : targetPoint.position;
        view.rotation = Interactable ? originPoint.rotation : targetPoint.rotation;
    }

    public override string GetOverviewInfo(VariableSystem variableSystem)
    {
        return overviewInfo;
    }
}

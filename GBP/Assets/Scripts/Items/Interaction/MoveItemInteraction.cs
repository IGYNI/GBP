using System.Collections;
using DG.Tweening;
using UnityEngine;

public class MoveItemInteraction : ItemInteraction
{
    [SerializeField] private string overviewInfo;
    [SerializeField] private Transform originPoint;
    [SerializeField] private Transform targetPoint;
    [SerializeField] private Transform view;
    [SerializeField] private GameAction OnInteract;
    [SerializeField] private float pushTime = 0.6f;

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
        variableSystem.SetVariable(item.info.itemName + Item.MovedSuffix, "true", true);
        OnInteract.Invoke(item.gameObject, variableSystem);
        StartCoroutine(PushCor());
        onInteract.Invoke();
    }

    private IEnumerator PushCor()
    {
        Interactable = false;
        yield return new WaitForSeconds(0.5f);
        view.DORotate(targetPoint.eulerAngles, pushTime);
        yield return view.DOMove(targetPoint.position, pushTime);
        _finished = true;
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

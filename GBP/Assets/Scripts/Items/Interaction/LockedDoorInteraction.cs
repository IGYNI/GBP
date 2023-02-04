using UnityEngine;

public class LockedDoorInteraction : ItemInteraction
{
	public override PlayerController.PlayerState ProvideState => PlayerController.PlayerState.IdleInteract;
	public override bool Interactable { get; protected set; }
	
	[SerializeField] private string overviewInfo;

	private void Awake()
	{
		Interactable = true;
	}

	public override void Interact(VariableSystem variableSystem)
	{
		
	}

	public override void LoadState(VariableSystem variableSystem)
	{
		
	}

	public override string GetOverviewInfo(VariableSystem variableSystem)
	{
		return overviewInfo;
	}
}

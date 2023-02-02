using UnityEngine;

[RequireComponent(typeof(Item))]
public abstract class ItemInteraction : MonoBehaviour
{
	[SerializeField] public Item item;
	public Outline outline;
	public InteractionInfo interactionInfo;
	
	private void OnValidate()
	{
		item = GetComponent<Item>();
	}

	public abstract PlayerController.PlayerState ProvideState { get; }
	public abstract bool Interactable { get; protected set; }
	public abstract void Interact(VariableSystem variableSystem);
	public abstract void LoadState(VariableSystem variableSystem);
	public abstract string GetOverviewInfo(VariableSystem variableSystem);
}

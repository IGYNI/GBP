using UnityEngine;

[RequireComponent(typeof(Item))]
public abstract class ItemInteraction : MonoBehaviour
{
	[SerializeField] public Item item;
	
	private void OnValidate()
	{
		item = GetComponent<Item>();
		outline = GetComponent<Outline>();
	}

	public abstract PlayerController.PlayerState ProvideState { get; }
	public abstract bool Interactable { get; protected set; }
	public abstract void Interact(VariableSystem variableSystem);
	public abstract void LoadState(VariableSystem variableSystem);

	public abstract string GetOverviewInfo(VariableSystem variableSystem);
	public Outline outline;
	public InteractionInfo interactionInfo;
}

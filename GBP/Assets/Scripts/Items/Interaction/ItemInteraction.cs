using UnityEngine;

[RequireComponent(typeof(Item))]
public abstract class ItemInteraction : MonoBehaviour
{
	[SerializeField] public Item item;
	[SerializeField] private float interactionTime = 1f;
	public Outline outline;
	public InteractionInfo interactionInfo;
	
	private void OnValidate()
	{
		item = GetComponent<Item>();
	}

	public float InteractionTime => interactionTime;
	public abstract PlayerController.PlayerState ProvideState { get; }
	public abstract bool Interactable { get; protected set; }
	public abstract bool Interact(VariableSystem variableSystem);
	public abstract void LoadState(VariableSystem variableSystem);
	public abstract string GetOverviewInfo(VariableSystem variableSystem);
}

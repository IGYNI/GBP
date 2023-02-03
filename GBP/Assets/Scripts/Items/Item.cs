using UnityEngine;

public class Item : MonoBehaviour
{
	public const string TakenSuffix = "_taken";
	public const string VisibleSuffix = "_visible";
	public const string ExploredSuffix = "_explored";
	public const string MovedSuffix = "_moved";

	public ItemInfo info;
	[SerializeField] public ItemInteraction interaction;

#if UNITY_EDITOR
	private void Awake()
	{
		if (info == null)
		{
			Debug.LogError($"[Item] {name} ItemInfo not set", gameObject);
		}
	}
#endif
	
	private void OnValidate()
	{
		interaction = GetComponent<ItemInteraction>();
	}
	
	public void LoadState(VariableSystem variableSystem)
	{
		GameVar variable = variableSystem.GetVariable(info.itemName + VisibleSuffix);
		if (variable != null)
		{
			gameObject.SetActive(variable.Value == "true");
		}

		if (interaction != null)
		{
			interaction.LoadState(variableSystem);
		}
	}
}

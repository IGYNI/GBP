using UnityEngine;

public class Item : MonoBehaviour
{
	public static readonly string TakenSuffix = "_taken";
	public static readonly string VisibleSuffix = "_visible";
	
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
	}
}

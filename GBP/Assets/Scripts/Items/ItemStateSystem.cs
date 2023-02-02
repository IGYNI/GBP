using System.Collections.Generic;
using UnityEngine;

public class ItemStateSystem : MonoBehaviour
{
	[SerializeField] private List<Item> items;

#if UNITY_EDITOR
	[ContextMenu("ScanItemsOnScene")]
	private void ScanItemsOnScene()
	{
		items.Clear();
		var sceneItems = FindObjectsOfType<Item>();
		items.AddRange(sceneItems);
	}
#endif

	public void Load()
	{
		var variableSystem = VariableSystem.Instance;
		foreach (Item item in items)
		{
			item.LoadState(variableSystem);
		}
	}
	
}

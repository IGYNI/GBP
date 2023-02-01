using System;
using UnityEngine;

public class Item : MonoBehaviour
{
	public string itemName;
	public string description;
	public Sprite icon;
	public Outline itemOutline;
	public InteractionInfo interactionInfo;
	
	private void OnValidate()
	{
		itemOutline = GetComponent<Outline>();
	}
}

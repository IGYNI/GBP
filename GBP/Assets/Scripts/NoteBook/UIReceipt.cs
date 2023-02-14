using System.Collections.Generic;
using UnityEngine.Localization.Components;
using UnityEngine;
using UnityEngine.UI;


public class UIReceipt : MonoBehaviour
{
	[SerializeField] private List<Image> receiptIngredients;
	[SerializeField] private Image receiptResult;
	[SerializeField] private LocalizeStringEvent receiptName;

	public void SetReceipt(Receipt receipt)
	{
		receiptName.SetTable("Notes");
		receiptName.SetEntry(receipt.screenName);
		for (int i = 0; i < receipt.requiredItems.Count; i++)
		{
			var img = receiptIngredients[i];
			img.enabled = true;
			img.sprite = receipt.requiredItems[i].icon;
		}

		receiptResult.sprite = receipt.result.icon;
	}
}
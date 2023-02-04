using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIReceipt : MonoBehaviour
{
	[SerializeField] private List<Image> receiptIngredients;
	[SerializeField] private Image receiptResult;
	[SerializeField] private TMP_Text receiptName;

	public void SetReceipt(Receipt receipt)
	{
		receiptName.text = receipt.screenName;
		for (int i = 0; i < receipt.requiredItems.Count; i++)
		{
			var img = receiptIngredients[i];
			img.enabled = true;
			img.sprite = receipt.requiredItems[i].icon;
		}

		receiptResult.sprite = receipt.result.icon;
	}
}
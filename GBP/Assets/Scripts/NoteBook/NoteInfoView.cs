using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NoteInfoView : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] private TextMesh note;
	[SerializeField] private GameObject receiptView;
	
	[SerializeField] private List<Image> receiptIngredients;
	[SerializeField] private Image receiptResult;

	public void SetNote(NoteInfo noteInfo)
	{
		note.text = noteInfo.text;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		
	}
}

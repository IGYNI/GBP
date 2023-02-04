using UnityEngine;
using UnityEngine.EventSystems;

public class NoteInfoView : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] private TMPro.TMP_Text note;
	[SerializeField] private UIReceipt receiptView;
	public NoteInfo NoteInfo => _noteInfo;
	private NoteInfo _noteInfo;

	public void SetNote(NoteInfo noteInfo)
	{
		_noteInfo = noteInfo;
		note.text = noteInfo.text;
		if (noteInfo.isReceipt)
		{
			receiptView.gameObject.SetActive(true);
			receiptView.SetReceipt(noteInfo.receipt);
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		
	}
}

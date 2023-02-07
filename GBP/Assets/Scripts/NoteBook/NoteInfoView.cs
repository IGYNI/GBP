using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NoteInfoView : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] private TMPro.TMP_Text header;
	[SerializeField] private TMPro.TMP_Text note;
	[SerializeField] private UIReceipt receiptView;
	[SerializeField] private Image highlight;
	public NoteInfo NoteInfo => _noteInfo;
	private NoteInfo _noteInfo;

	public void SetNote(NoteInfo noteInfo)
	{
		header.text = noteInfo.screenName;
		_noteInfo = noteInfo;
		note.text = noteInfo.text;
		if (noteInfo.isReceipt)
		{
			receiptView.gameObject.SetActive(true);
			receiptView.SetReceipt(noteInfo.receipt);
		}

		highlight.DOFade(0f, 1f).SetEase(Ease.OutBounce);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		
	}
}

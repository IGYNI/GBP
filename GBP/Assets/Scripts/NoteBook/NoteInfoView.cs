using DG.Tweening;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class NoteInfoView : MonoBehaviour
{
	[SerializeField] private LocalizeStringEvent localizedHeader;
	[SerializeField] private LocalizeStringEvent localizedNote;
	[SerializeField] private UIReceipt receiptView;
	[SerializeField] private Image highlight;
	public NoteInfo NoteInfo => _noteInfo;
	private NoteInfo _noteInfo;

	public void SetNote(NoteInfo noteInfo)
	{
		_noteInfo = noteInfo;
		localizedHeader.SetTable("Notes");
		localizedHeader.SetEntry(noteInfo.screenName);
		localizedNote.SetTable("Notes");
		localizedNote.SetEntry(noteInfo.text);
		
		if (noteInfo.isReceipt)
		{
			receiptView.gameObject.SetActive(true);
			receiptView.SetReceipt(noteInfo.receipt);
		}

		highlight.DOFade(0f, 1f).SetEase(Ease.OutBounce);
	}
}

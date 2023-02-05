using UnityEngine;
using UnityEngine.EventSystems;

public class NoteHeader : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] private TextMesh noteHeader;
	private NoteInfo NoteInfo => _noteInfo;
	private NoteInfo _noteInfo;

	public void SetNote(NoteInfo noteInfo)
	{
		_noteInfo = noteInfo;
		noteHeader.text = noteInfo.noteName;
	}
	
	public void OnPointerClick(PointerEventData eventData)
	{
		if (_noteInfo.isReceipt)
		{
			//TODO
		}
	}
}
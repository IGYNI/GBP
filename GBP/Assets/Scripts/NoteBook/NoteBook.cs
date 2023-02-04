using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteBook : MonoBehaviour
{
	public event Action<NoteInfo> OnAddNote;

	[SerializeField] private GameObject view;
	[SerializeField] private GameObject infoParent;
	[SerializeField] private Button showNotebook;
	[SerializeField] private Button closeNoteBook;

	[SerializeField] private NoteInfoView _noteInfoViewPreset;
	public List<NoteInfo> Notes => _notes;
	
	private readonly List<NoteInfo> _notes = new List<NoteInfo>();

	//private List<NoteInfoView> _noteViews = new List<NoteInfoView>();

	private void Awake()
	{
		showNotebook.onClick.AddListener(OnShowClick);
		closeNoteBook.onClick.AddListener(OnCloseClick);
	}

	public void Add(NoteInfo noteInfo)
	{
		Debug.Log($"added note {noteInfo.noteName}");
		_notes.Add(noteInfo);
		var noteView = Instantiate(_noteInfoViewPreset, infoParent.transform);
		noteView.SetNote(noteInfo);
		//_noteViews.Add(noteView);
		OnAddNote?.Invoke(noteInfo);
		OnShowClick();
	}

	private void OnShowClick()
	{
		view.gameObject.SetActive(true);
	}
	
	private void OnCloseClick()
	{
		view.gameObject.SetActive(false);
	}
}

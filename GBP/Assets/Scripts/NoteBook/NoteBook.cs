using System;
using System.Collections.Generic;
using UnityEngine;

public class NoteBook : MonoBehaviour
{
	public event Action<NoteInfo> OnAddVariable;

	[SerializeField] private GameObject view;
	[SerializeField] private GameObject infoParent;
	
	[SerializeField] private NoteInfoView _noteInfoViewPreset;

	private readonly List<NoteInfo> _notes = new List<NoteInfo>();

	private List<NoteInfoView> _noteViews;

	public void Add(NoteInfo noteInfo)
	{
		Debug.Log($"added note {noteInfo.noteName}");
		OnAddVariable?.Invoke(noteInfo);
		_notes.Add(noteInfo);
	}

	public void ShowNoteBook()
	{
		
	}

	public void HideNoteBook()
	{
		
	}
}

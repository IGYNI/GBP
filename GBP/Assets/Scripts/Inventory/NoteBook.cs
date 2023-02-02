using System;
using UnityEngine;

public class NoteBook : MonoBehaviour
{
	public event Action<NoteInfo> OnAddVariable;
	
	public void Add(NoteInfo noteInfo)
	{
		Debug.Log($"added note {noteInfo.noteName}");
		OnAddVariable?.Invoke(noteInfo);
	}
}

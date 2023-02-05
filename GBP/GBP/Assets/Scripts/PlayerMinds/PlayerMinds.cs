using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerMinds : ScriptableObject
{
	[Serializable]
	public class Entry
	{
		public string name;
		public Sprite sprite;
		
	}

	public List<Entry> minds;

	public Entry GetMind(string mindName)
	{
		var result = minds.Find(p => p.name == mindName);
		return result;
	}
}

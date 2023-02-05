using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIVariableDesc : MonoBehaviour
{
	[SerializeField] private TMP_Text varName;
	[SerializeField] private TMP_Text varValue;

	private GameVar _gameVar;
	public void Init(GameVar gameVar)
	{
		_gameVar = gameVar;
		UpdateInfo(_gameVar, _gameVar.Value);
		_gameVar.OnValueChanged += UpdateInfo;
	}

	private void UpdateInfo(GameVar gameVar, string newValue)
	{
		varName.text = gameVar.Name;
		varValue.text = gameVar.Value;
	}
	
}

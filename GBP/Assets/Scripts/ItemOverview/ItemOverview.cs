using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;

public class ItemOverview : MonoBehaviour
{
	//[SerializeField] private LocalizeStringEvent overview;
	[SerializeField] private TMP_Text overviewText;
	[SerializeField] private float showTime = 5f;

	private bool _isShow;
	private float _showTimer;
	private float _wait;
	private ItemInteraction _itemInteraction;

	private void Start()
	{
		//overview.SetTable("Notes");
	}

	public async UniTask ShowOverview(string text, float time = -1)
	{
		if (string.IsNullOrEmpty(text))
		{
			_isShow = false;
			_showTimer = 0f;
			overviewText.text = "";
		}
		else
		{
			_isShow = true;
			_showTimer = 0f;
			_wait = time;
			overviewText.text = await LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Notes", text);
			//overview.SetEntry(text);
			if (time <= -1)
			{
				_wait = showTime;
			}
		}
	}

	public async UniTask SetItemInfo(ItemInfo itemInfo)
	{
		if (itemInfo != null)
		{
			overviewText.text = await LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Notes", itemInfo.description);
			//overview.SetEntry(itemInfo.description);
		}
		else
		{
			ShowOverview("");
			//overviewText.text = "";
		}
	}

	public void SetItemInfo(ItemInteraction itemInteraction)
	{
		_itemInteraction = itemInteraction;
		if (_itemInteraction != null)
		{
			var info = itemInteraction.GetOverviewInfo(VariableSystem.Instance);
#if UNITY_EDITOR
			if (string.IsNullOrEmpty(info))
			{
				Debug.LogWarning($"[ItemOverview] Overview not set on {itemInteraction.gameObject.name}");
				UnityEditor.Selection.activeGameObject = itemInteraction.gameObject;
			}
#endif
			ShowOverview(info);
		}
		else
		{
			ShowOverview("");
			_isShow = false;
			_showTimer = 0f;
		}
	}

	private void Update()
	{
		if (!_isShow)
			return;
		_showTimer += Time.deltaTime;

		if (_showTimer >= _wait)
		{
			_isShow = false;
			ShowOverview("");
		}
	}
}
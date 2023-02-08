using TMPro;
using UnityEngine;

public class ItemOverview : MonoBehaviour
{
	[SerializeField] private TMP_Text overviewText;
	[SerializeField] private float showTime = 5f;

	private bool _isShow;
	private float _showTimer;
	private float _wait;
	private ItemInteraction _itemInteraction;

	public void ShowOverview(string text, float time = -1)
	{
		// if (string.IsNullOrEmpty(text))
		// 	return;
		
		_isShow = true;
		_showTimer = 0f;
		_wait = time;
		overviewText.text = text;
		if (time <= -1)
		{
			_wait = showTime;
		}
	}

	public void SetItemInfo(ItemInfo itemInfo)
	{
		if (itemInfo != null)
		{
			overviewText.text = itemInfo.description;
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
			overviewText.text = "";
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
			overviewText.text = "";
		}
	}
}
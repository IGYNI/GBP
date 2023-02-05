using TMPro;
using UnityEngine;

public class ItemOverview : MonoBehaviour
{
	[SerializeField] private TMP_Text overviewText;
	[SerializeField] private float overviewTime = 3f;

	private bool _isShow;
	private float _showTimer;
	private float _wait;
	
	public void ShowOverview(string text, float time = -1)
	{
		if (string.IsNullOrEmpty(text))
			return;
		
		_isShow = true;
		_showTimer = 0f;
		_wait = time;
		overviewText.text = text;
		if (time <= -1)
		{
			_wait = overviewTime;
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
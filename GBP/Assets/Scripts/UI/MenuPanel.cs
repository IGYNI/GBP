using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace UI
{
	public class MenuPanel : MonoBehaviour
	{
		[SerializeField] private CanvasGroup canvasGroup;
		[SerializeField] private float fadeTime = 0.3f;
		[SerializeField] private MenuPanel previous;

		public IEnumerator Show()
		{
			yield return canvasGroup.DOFade(1f, fadeTime).SetEase(Ease.OutSine);
			canvasGroup.interactable = true;
			canvasGroup.blocksRaycasts = true;
		}
		
		public IEnumerator Hide()
		{
			canvasGroup.blocksRaycasts = false;
			canvasGroup.interactable = false;
			yield return canvasGroup.DOFade(0f, fadeTime).SetEase(Ease.InSine);
		}

		private IEnumerator BackSequence(MenuPanel prev)
		{
			yield return Hide();
			yield return prev.Show();
		}

		public void Back()
		{
			if (previous != null)
			{
				StartCoroutine(BackSequence(previous));
			}
		}
	}
}
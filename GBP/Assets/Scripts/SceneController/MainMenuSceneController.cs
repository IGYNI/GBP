using System;
using Cysharp.Threading.Tasks;
using SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SceneController
{
	public class MainMenuSceneController : BaseSceneController
	{
		public UnityEvent OnLoad;

		[SerializeField] private Button newGameButton;

		private void Awake()
		{
			newGameButton.onClick.AddListener(OnNewGameButtonClick);
		}

		public override async UniTask Load(SceneContext sceneContext, IProgress<LoadingProgress> progress)
		{
			await UniTask.Yield();
			OnLoad.Invoke();
			progress.Report(new LoadingProgress() { progress = 1f });
		}

		private void OnNewGameButtonClick()
		{
			var context = new SceneContext();
			var sceneInfo = new SerializedSceneInfo();
			sceneInfo.sceneName = "StoryCutScene";
			context.sceneInfo = sceneInfo;
			SceneLoader.LoadScene(context);
		}
	}
}
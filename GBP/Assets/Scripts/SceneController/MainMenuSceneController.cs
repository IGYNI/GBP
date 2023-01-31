using System;
using Cysharp.Threading.Tasks;
using SceneManagement;
using UnityEngine.Events;

namespace SceneController
{
	public class MainMenuSceneController : BaseSceneController
	{
		public UnityEvent OnLoad;

		public override async UniTask Load(SceneContext sceneContext, IProgress<LoadingProgress> progress)
		{
			await UniTask.Yield();
			OnLoad.Invoke();
			progress.Report(new LoadingProgress() { progress = 1f });
		}
	}
}
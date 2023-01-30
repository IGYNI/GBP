using System;
using Cysharp.Threading.Tasks;
using SceneManagement;
using UnityEngine.Events;

public class DockingRoomSceneBootstrap : BaseSceneController
{
	public UnityEvent OnLoad;

	//[SerializeField] private PlayerController player;
	
	public override async UniTask Load(SceneContext sceneContext, IProgress<LoadingProgress> progress)
	{
		await UniTask.Yield();
		OnLoad.Invoke();
		//TODO place player, initialize items;
		progress.Report(new LoadingProgress(){progress = 1f});
	}
}

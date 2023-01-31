using System;
using Cysharp.Threading.Tasks;
using SceneManagement;

public class KitchenRoomBootstrap : RoomSceneController
{
	public override async UniTask Load(SceneContext sceneContext, IProgress<LoadingProgress> progress)
	{
		await UniTask.Yield();
		OnLoad.Invoke();
		SpawnPlayer(sceneContext);
		//TODO initialize items;
		progress.Report(new LoadingProgress() { progress = 1f });
	}
}
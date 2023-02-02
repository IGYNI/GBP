using System;
using Cysharp.Threading.Tasks;
using SceneManagement;

public class DockingRoomBootstrap : RoomSceneController
{
	public override async UniTask Load(SceneContext sceneContext, IProgress<LoadingProgress> progress)
	{
		await LoadRoom(sceneContext, progress);
	}
}
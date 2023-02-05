using System;
using Cysharp.Threading.Tasks;
using SceneManagement;

public class LaboratoryRoomBootstrap : RoomSceneController
{
	public override async UniTask Load(SceneContext sceneContext, IProgress<LoadingProgress> progress)
	{
		await LoadRoom(sceneContext, progress);
	}
}
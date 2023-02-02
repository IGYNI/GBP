using System;
using Cysharp.Threading.Tasks;
using SceneManagement;

public class GeneratorRoomBootstrap : RoomSceneController
{
	public override async UniTask Load(SceneContext sceneContext, IProgress<LoadingProgress> progress)
	{
		await LoadRoom(sceneContext, progress);
	}
}
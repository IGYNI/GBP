using System;
using Cysharp.Threading.Tasks;
using SceneManagement;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ItemStateSystem))]
[RequireComponent(typeof(PlayerSpawner))]
public abstract class RoomSceneController : BaseSceneController
{
	public UnityEvent OnLoad;

	[SerializeField] private PlayerController player;
	[SerializeField] private PlayerSpawner playerSpawner;
	[SerializeField] private ItemStateSystem itemStateSystem;

	private void OnValidate()
	{
		itemStateSystem = GetComponent<ItemStateSystem>();
		playerSpawner = GetComponent<PlayerSpawner>();
	}

	protected async UniTask LoadRoom(SceneContext sceneContext, IProgress<LoadingProgress> progress)
	{
		progress.Report(new LoadingProgress() { progress = 0.3f });
		await UniTask.Yield();
		OnLoad.Invoke();
		progress.Report(new LoadingProgress() { progress = 0.4f });
		await UniTask.Yield();
		if (sceneContext is RoomSceneContext roomSceneContext)
		{
			var location = playerSpawner.GetSpawnLocation(roomSceneContext.spawnPointName);
			player.transform.position = location.position;
			player.transform.forward = location.forward;
		}
		progress.Report(new LoadingProgress() { progress = 0.6f });
		await UniTask.Yield();

		itemStateSystem.Load();
		progress.Report(new LoadingProgress() { progress = 1f });
		await UniTask.Yield();
	}
}
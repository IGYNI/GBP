using SceneManagement;
using UnityEngine;
using UnityEngine.Events;

public abstract class RoomSceneController : BaseSceneController
{
	public UnityEvent OnLoad;

	[SerializeField] private PlayerController player;
	[SerializeField] private PlayerSpawner playerSpawner;

	protected void SpawnPlayer(SceneContext sceneContext)
	{
		if (sceneContext is RoomSceneContext roomSceneContext)
		{
			var location = playerSpawner.GetSpawnLocation(roomSceneContext.spawnPointName);
			player.transform.position = location.position;
			player.transform.forward = location.forward;
		}
	}
}
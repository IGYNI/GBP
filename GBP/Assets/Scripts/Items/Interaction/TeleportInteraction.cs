using System;
using System.Collections;
using SceneManagement;
using UnityEngine;

public class TeleportInteraction : ItemInteraction
{
	public override PlayerController.PlayerState ProvideState => PlayerController.PlayerState.TakeItem;
	public override bool Interactable { get; protected set; }
	[SerializeField] private string overview;
	
	public string exitPointName; 
	public SerializedSceneInfo nextSceneInfo;

	private void Awake()
	{
		Interactable = true;
	}

	public override void Interact(VariableSystem variableSystem)
	{
		StartCoroutine(LoadNextLevelCor());
	}
	
	private IEnumerator LoadNextLevelCor()
	{
		yield return new WaitForSecondsRealtime(0.6f);
		LoadNextScene();
	}
	
	private void LoadNextScene()
	{
		var nextSceneContext = new RoomSceneContext();
		nextSceneContext.sceneInfo = nextSceneInfo;
		nextSceneContext.spawnPointName = exitPointName;
		SceneLoader.LoadScene(nextSceneContext);
	}

	public override void LoadState(VariableSystem variableSystem)
	{
		
	}

	public override string GetOverviewInfo(VariableSystem variableSystem)
	{
		return overview;
	}
}

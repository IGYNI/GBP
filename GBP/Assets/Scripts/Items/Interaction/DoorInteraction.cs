using System.Collections;
using SceneManagement;
using UnityEngine;

public class DoorInteraction : ItemInteraction
{
	public override PlayerController.PlayerState ProvideState => PlayerController.PlayerState.Interact;
	public override bool Interactable { get; protected set; }
	
	[SerializeField] private string failInfo;
	[SerializeField] private string overviewInfo;

	[SerializeField] private CheckCondition openDoorCondition;
	[SerializeField] private AutomaticDoor door;
	
	
	public string exitPointName; 
	public SerializedSceneInfo nextSceneInfo;

	private void Awake()
	{
		Interactable = true;
	}

	public override bool Interact(VariableSystem variableSystem)
	{
		if (openDoorCondition == null)
		{
			StartCoroutine(LoadNextLevelCor());
		}
		else
		{
			if (openDoorCondition.Satisfied())
			{
				StartCoroutine(LoadNextLevelCor());
			}
		}
		return true;
	}

	private IEnumerator LoadNextLevelCor()
	{
		yield return new WaitForSecondsRealtime(0.3f);
		door.Open();
		yield return new WaitForSecondsRealtime(0.5f);
		LoadNextScene();
	}

	public override void LoadState(VariableSystem variableSystem)
	{
		
	}

	public override string GetOverviewInfo(VariableSystem variableSystem)
	{
		return openDoorCondition.Satisfied() ? failInfo : overviewInfo;
	}

	private void LoadNextScene()
	{
		var nextSceneContext = new RoomSceneContext();
		nextSceneContext.sceneInfo = nextSceneInfo;
		nextSceneContext.spawnPointName = exitPointName;
		SceneLoader.LoadScene(nextSceneContext);
	}
}

using System.Collections;
using SceneManagement;
using UnityEngine;

public class DoorInteraction : ItemInteraction
{
	public override PlayerController.PlayerState ProvideState
	{
		get 
		{
			if (openDoorCondition != null && !openDoorCondition.Satisfied())
			{
				return PlayerController.PlayerState.IdleInteract;	
			}
			return PlayerController.PlayerState.Interact; 
		}
	}

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

	public override void Interact(VariableSystem variableSystem)
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
	}

	private IEnumerator LoadNextLevelCor()
	{
		onInteract.Invoke();
		outline.enabled = false;
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
		if (openDoorCondition != null)
		{
			return openDoorCondition.Satisfied() ? overviewInfo : failInfo;
		}
		return overviewInfo;
	}

	private void LoadNextScene()
	{
		var nextSceneContext = new RoomSceneContext();
		nextSceneContext.sceneInfo = nextSceneInfo;
		nextSceneContext.spawnPointName = exitPointName;
		SceneLoader.LoadScene(nextSceneContext);
	}
}

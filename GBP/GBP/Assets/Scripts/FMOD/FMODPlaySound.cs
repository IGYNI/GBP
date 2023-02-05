using UnityEngine;

public class FMODPlaySound : MonoBehaviour
{
	public FMODUnity.EventReference eventReference;

	public void Play()
	{
		var playerState = FMODUnity.RuntimeManager.CreateInstance(eventReference);
		playerState.start();
	}
}
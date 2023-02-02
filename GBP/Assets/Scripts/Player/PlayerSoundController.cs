using System.Collections;
using FMOD.Studio;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerSoundController : MonoBehaviour
{
	private PlayerController _player;
	private EventInstance _footsteps;

	[SerializeField] private float sceneFootstepsValue;
	
	private void Awake()
	{
		_player = GetComponent<PlayerController>();
		_player.State.OnValueChanged += HandlePlayerState;
	}

	private void Start()
	{
		_footsteps = AudioManager.instance.PlayerFootsteps;
		_footsteps.setParameterByName("footsteps", sceneFootstepsValue);
	}

	private void OnDestroy()
	{
		_player.State.OnValueChanged -= HandlePlayerState;
		_footsteps.stop(STOP_MODE.IMMEDIATE);
	}

	private void HandlePlayerState(PlayerController.PlayerState prev, PlayerController.PlayerState current)
	{
		
	}

	private void Update()
	{
		UpdateSounds();
	}
	
	private void UpdateSounds()
	{
		_footsteps.getPlaybackState(out PLAYBACK_STATE playbackState);
		if (_player.State.Value == PlayerController.PlayerState.Run && playbackState == PLAYBACK_STATE.STOPPED)
		{
			_footsteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
			_footsteps.start();
		}
		
		// else if (_sceneName == "Corridor")
		// {
		// 	_footsteps.setParameterByName("footsteps", 1);
		// }
		// else
		// {
		// 	_footsteps.setParameterByName("footsteps", 0);
		// }
	}
}

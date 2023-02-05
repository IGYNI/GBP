using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class VideoPlayer : MonoBehaviour
{
	[SerializeField] private UnityEvent OnPlaybackComplete;
	[SerializeField] private string videoFileName;

	private void Start()
	{
		GameObject camera = GameObject.Find("Main Camera");

		// VideoPlayer automatically targets the camera backplane when it is added
		// to a camera object, no need to change videoPlayer.targetCamera.
		var videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();
		videoPlayer.playOnAwake = false;

		// By default, VideoPlayers added to a camera will use the far plane.
		// Let's target the near plane instead.
		videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;

		// This will cause our Scene to be visible through the video being played.
		videoPlayer.targetCameraAlpha = 0.5F;

		// Set the video to play. URL supports local absolute or relative paths.
		// Here, using absolute.
		//var path = Application.streamingAssetsPath + "/+""CutScene.mp4";
		var path = Application.streamingAssetsPath + "/" + videoFileName;
		if (File.Exists(path))
		{
			videoPlayer.url = path;
			videoPlayer.loopPointReached += EndReached;
			videoPlayer.Play();
		}
		else
		{
			OnPlaybackComplete.Invoke();
		}

		// Start playback. This means the VideoPlayer may have to prepare (reserve
		// resources, pre-load a few frames, etc.). To better control the delays
		// associated with this preparation one can use videoPlayer.Prepare() along with
		// its prepareCompleted event.
	}

	private void EndReached(UnityEngine.Video.VideoPlayer vp)
	{
		OnPlaybackComplete.Invoke();
	}
}
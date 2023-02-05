using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameOverSceneController : BaseSceneController
{
	[SerializeField] private GameObject skipText;
	[SerializeField] private SerializedSceneInfo nextSceneInfo;
	[SerializeField] private float cutsceneTime;

	private bool _skip;
	private bool _loadComplete;
	private Coroutine _cutSceneCor;

	private void Awake()
	{
		skipText.gameObject.SetActive(false);
	}

	private void Update()
	{
		if (_skip)
			return;
		if (!_loadComplete)
			return;

		if (Input.GetKeyDown(KeyCode.Space))
		{
			_skip = true;
			StopCoroutine(_cutSceneCor);
			LoadNextScene();
		}
	}

	private IEnumerator CutSceneCor()
	{
		yield return new WaitForSecondsRealtime(3);
		skipText.gameObject.SetActive(true);
		yield return new WaitForSecondsRealtime(30);
		LoadNextScene();
	}

	public void LoadNextScene()
	{
		var context = new SceneContext();
		context.sceneInfo = nextSceneInfo;
		SceneLoader.LoadScene(context);
	}

	public override async UniTask Load(SceneContext sceneContext, IProgress<LoadingProgress> progress)
	{
		await UniTask.Yield();
		progress.Report(new LoadingProgress() { progress = 1f });
	}

	public override void OnLoadComplete()
	{
		_cutSceneCor = StartCoroutine(CutSceneCor());
		_loadComplete = true;
	}
}
using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using SceneManagement;
using UnityEngine;

public class CutSceneController : BaseSceneController
{
	[SerializeField] private GameObject skipText;
	[SerializeField] private SerializedSceneInfo nextSceneInfo;
	[SerializeField] private float waitSkipTimer = 3;

	private bool _skip;
	private bool _loadComplete;
	private Coroutine _cutSceneCor;

	private void Awake()
	{
		skipText.gameObject.SetActive(false);
	}

	private void Start()
	{
		if (VariableSystem.Instance != null)
		{
			Destroy(VariableSystem.Instance.gameObject);
		}
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
		yield return new WaitForSecondsRealtime(waitSkipTimer);
		skipText.gameObject.SetActive(true);
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
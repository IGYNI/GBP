using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using SceneManagement;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CreditsSceneController : BaseSceneController
{
	[SerializeField] private GameObject skipText;
	[SerializeField] private SerializedSceneInfo nextSceneInfo;
	[SerializeField] private Button skip;
	[SerializeField] private Scrollbar scrollbar;
	[SerializeField] private float speedScrollSpeed;


	private float _timer;
	private bool _skip;
	private bool _started;
	private bool _loadComplete;
	private Coroutine _cutSceneCor;

	private void Awake()
	{
		skipText.gameObject.SetActive(false);
		skip.onClick.AddListener(OnSkipButtonClick);
	}

	[ContextMenu("Emit")]
	private void Emit()
	{
		OnLoadComplete();
	}

	private void Update()
	{
		if (!_loadComplete)
			return;

		_timer += Mathf.Clamp01(Time.deltaTime * speedScrollSpeed);
		scrollbar.value = 1f-_timer;
		
		if (_skip)
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

	private void OnSkipButtonClick()
	{
		_skip = true;
		LoadNextScene();
	}
}
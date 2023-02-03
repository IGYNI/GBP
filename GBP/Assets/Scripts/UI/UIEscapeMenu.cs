using System;
using System.Collections;
using SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
	public class UIEscapeMenu : MonoBehaviour
	{
		[SerializeField] private MenuPanel mainPanel;
		[SerializeField] private MenuPanel settingsPanel;
		[SerializeField] private Button newGameButton;
		[SerializeField] private Button continueButton;
		[SerializeField] private Button settingsButton;
		[SerializeField] private Button exitGameButton;
		[SerializeField] private Button closeMenuButton;
		[SerializeField] private GameObject view;
		[SerializeField] private GameObject dimmer;

		private string _sceneName;
		
		private void Awake()
		{
			newGameButton.onClick.AddListener(OnNewGameButtonClick);
			continueButton.onClick.AddListener(OnContinueButtonClick);
			settingsButton.onClick.AddListener(OnSettingButtonClick);
			exitGameButton.onClick.AddListener(OnExitButtonClick);
			closeMenuButton.onClick.AddListener(OnCloseButtonClick);
		}

		private IEnumerator Start()
		{
			yield return mainPanel.Show();
			yield return settingsPanel.Hide();
			_sceneName = SceneManager.GetActiveScene().name;
			var isMainMenu = _sceneName == "MainMenu";
			view.gameObject.SetActive(isMainMenu);
			dimmer.gameObject.SetActive(!isMainMenu);
			newGameButton.gameObject.SetActive(isMainMenu);
			//continueButton.gameObject.SetActive(isMainMenu);
			continueButton.gameObject.SetActive(false);
			closeMenuButton.gameObject.SetActive(!isMainMenu);
		}

		private void OnNewGameButtonClick()
		{
			view.gameObject.SetActive(false);
			var context = new SceneContext();
			var sceneInfo = new SerializedSceneInfo();
			sceneInfo.sceneName = "StoryCutScene";
			context.sceneInfo = sceneInfo;
			SceneLoader.LoadScene(context);
		}

		private void Update()
		{
			if (_sceneName == "MainMenu") 
				return;
			
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				if (view.activeSelf)
				{
					view.SetActive(false);
				}
				else
				{
					view.SetActive(true);
				}
			}
		}

		private void OnContinueButtonClick()
		{
			//TODO
		}

		private void OnSettingButtonClick()
		{
			StartCoroutine(OpenSettingsCor());
		}

		private IEnumerator OpenSettingsCor()
		{
			yield return mainPanel.Hide();
			yield return settingsPanel.Show();
		}

		private void OnExitButtonClick()
		{
			if (Application.isEditor)
			{
#if UNITY_EDITOR
				UnityEditor.EditorApplication.isPlaying = false;
#endif
			}
			else
			{
				Application.Quit();
			}
		}

		private void OnCloseButtonClick()
		{
			view.SetActive(false);
		}
	}
}
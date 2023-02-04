using System;
using Cysharp.Threading.Tasks;
using SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameOverSceneController : BaseSceneController
{
    [SerializeField] private SerializedSceneInfo nextSceneInfo;
    [SerializeField] private Button exitToMainMenu;

    private void Awake()
    {
        exitToMainMenu.onClick.AddListener(LoadNextScene);
    }

    private void LoadNextScene()
    {
        var context = new SceneContext();
        context.sceneInfo = nextSceneInfo;
        SceneLoader.LoadScene(context);
    }

    public override async  UniTask Load(SceneContext sceneContext, IProgress<LoadingProgress> progress)
    {
        if (VariableSystem.Instance != null)
        {
            Destroy(VariableSystem.Instance.gameObject);
        }
        Debug.Log($"[VariableSystem] Instance is {VariableSystem.Instance}");
        await UniTask.Yield();
    }
}

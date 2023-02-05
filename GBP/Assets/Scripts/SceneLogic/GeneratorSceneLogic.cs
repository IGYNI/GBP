using SceneManagement;
using UnityEngine;

public class GeneratorSceneLogic : MonoBehaviour
{
    [SerializeField] private SerializedSceneInfo nextScene;

    public void GameOver()
    {
        if (VariableSystem.Instance != null)
        {
            Destroy(VariableSystem.Instance.gameObject);
        }
        var context = new SceneContext();
        context.sceneInfo = nextScene;
        SceneLoader.LoadScene(context);
    }
}

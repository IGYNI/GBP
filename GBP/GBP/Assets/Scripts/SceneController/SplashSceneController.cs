using System.Collections;
using SceneManagement;
using UnityEngine;

public class SplashSceneController : MonoBehaviour
{
    [SerializeField] private SerializedSceneInfo nextSceneInfo;
    [SerializeField] private float showTime = 2f;
    
    private bool skip;
    private Coroutine cutSceneCor;

    private void Start()
    {
        cutSceneCor = StartCoroutine(CutSceneCor());
    }

    // Update is called once per frame
    private void Update()
    {
        if (skip) 
            return;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            skip = true;
            StopCoroutine(cutSceneCor);
            LoadNextScene();
        }
    }

    private IEnumerator CutSceneCor()
    {
        yield return new WaitForSecondsRealtime(showTime);
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        var context = new SceneContext();
        context.sceneInfo = nextSceneInfo;
        SceneLoader.LoadScene(context);
    }
}

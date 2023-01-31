using SceneManagement;
using UnityEngine;

public class ExitRoomTrigger : MonoBehaviour
{
    public string exitPointName; 
    public SerializedSceneInfo nextSceneInfo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var nextSceneContext = new RoomSceneContext();
            nextSceneContext.sceneInfo = nextSceneInfo;
            nextSceneContext.spawnPointName = exitPointName;
            SceneLoader.LoadScene(nextSceneContext);
        }
    }
}

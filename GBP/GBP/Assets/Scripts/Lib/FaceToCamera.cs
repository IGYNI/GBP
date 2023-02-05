using UnityEngine;

public class FaceToCamera : MonoBehaviour
{
    [SerializeField] private Camera cam;
   
    private void Start()
    {
        cam = Camera.main;
        transform.forward = cam.transform.forward;
    }
}

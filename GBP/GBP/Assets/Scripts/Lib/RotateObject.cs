using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private Axis axis;
    public float speed;

    private void Update()
    {
        switch (axis)
        {
            case Axis.X:
                transform.Rotate(Vector3.right, speed * Time.deltaTime);
                break;
            
            case Axis.Y:
                transform.Rotate(Vector3.up, speed * Time.deltaTime);
                break;
            
            case Axis.Z:
                transform.Rotate(Vector3.forward, speed * Time.deltaTime);
                break;
        }
    }
}

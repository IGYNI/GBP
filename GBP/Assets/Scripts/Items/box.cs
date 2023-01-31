using UnityEngine;

public class box : MonoBehaviour
{
    public Vector3 destination;
    public Outline Box_Outline;
    public bool IsOnPosition = false;

    private void Awake() 
    {
        Box_Outline = GetComponent<Outline>();    
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Camera : MonoBehaviour
{
    private Vector3 MousePos;
    private Transform Cam;


    public float Speed;

    private void Awake() 
    {
        Cam = GetComponent<Transform>();
    }
    
    void FixedUpdate()
    {
        MousePos = Input.mousePosition;
            Debug.Log("каеф");
            if (MousePos.y > 100 && Cam.position.y < -12)
                Cam.position = new Vector3(Cam.position.x, Cam.position.y + Speed, Cam.position.z);
            if (MousePos.y < 980 && Cam.position.y > -1.85f)
                Cam.position = new Vector3(Cam.position.x, Cam.position.y - Speed, Cam.position.z);
    }
}

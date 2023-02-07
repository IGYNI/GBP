using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Camera : MonoBehaviour
{
    private Vector3 MousePos;
    private Transform Cam;

    public Transform Box;


    public float Speed;

    private void Awake() 
    {
        Cam = GetComponent<Transform>();
    }
    
    void FixedUpdate()
    {
        MousePos = Input.mousePosition;
            if (MousePos.y > 100 && Cam.position.y < 2)
                Cam.position = new Vector3(Cam.position.x, Cam.position.y + Speed, Cam.position.z);
            if (MousePos.y < 980 && Cam.position.y > Box.position.y + -0.5f)
                Cam.position = new Vector3(Cam.position.x, Cam.position.y - Speed, Cam.position.z);
    }
}

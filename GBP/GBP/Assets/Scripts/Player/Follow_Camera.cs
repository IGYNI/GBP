using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Camera : MonoBehaviour
{
    private Transform Cam;
    public Transform Player;
    public Vector3 Dist;

    void Awake() 
    {
        Cam = GetComponent<Transform>();
    }

    void Update()
    {
        Cam.position = Player.position + Dist;
    }
}

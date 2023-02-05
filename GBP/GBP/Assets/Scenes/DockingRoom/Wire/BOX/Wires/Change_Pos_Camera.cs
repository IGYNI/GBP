using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Pos_Camera : MonoBehaviour
{
    private Transform Cam;
    public Vector3 StartPos;
    private Vector3 StartRot;
    public Transform Box;
    public Vector3 Destit;
    public bool IsOnY = false;
    public bool IsMove = false;
    public bool IsRotate = false;

    public float Speed;
    public float SPedR;


    void Start() 
    {
        Cam = GetComponent<Transform>();    
        StartPos = Cam.position;
        StartRot = new Vector3(Cam.rotation.x, Cam.rotation.y, Cam.rotation.z);
    }

    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            if (IsOnY == false)
            {
                GetComponent<Mr_Move_things>().Move(Cam, Destit, Speed);
                Cam.rotation = Quaternion.Euler(90, 0, 0);
                IsOnY = true;
                IsMove = true;
            }
            else if(IsOnY)
            {
                GetComponent<Mr_Move_things>().Move(Cam, StartPos, Speed);
                Cam.rotation = Quaternion.Euler(0, 0, 0);
                IsOnY = false;
                IsMove = true;
            }
        }
        else if(Input.GetKeyDown("y"))
        {
            if (IsRotate == false && IsOnY)
            {
                Cam.rotation = Quaternion.Euler(0, 0, 0);
                IsRotate = true;
            }
            if (IsRotate == false && IsOnY)
            {
                Cam.rotation = Quaternion.Euler(90, 0, 0);
                IsRotate = true;
            }
        }
        if (IsOnY && IsMove)
        {
            GetComponent<Mr_Move_things>().Move(Cam, new Vector3(Destit.x , Destit.y, Destit.z), Speed);
            //GetComponent<Mr_Move_things>().Rotate(Cam, new Vector3(-90, 0, 0), SPedR);
            if (Cam.position.z >= Destit.z )
                IsMove = false;

        }
        else if (IsOnY == false && IsMove)
        {
            GetComponent<Mr_Move_things>().Move(Cam, StartPos, Speed);
            //GetComponent<Mr_Move_things>().Rotate(Cam, new Vector3(0, 0, 0), SPedR);
            if (Cam.position.z == 3.39f)
                IsMove = false;
        }
    }
}

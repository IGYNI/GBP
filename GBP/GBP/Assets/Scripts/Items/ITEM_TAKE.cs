using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Item))]
public class ITEM_TAKE : MonoBehaviour
{
    public PLAYER_MOVE Player; 
    public UIItemInfo itemInfo; 
    public Transform Cam;
    public Vector3 Cam_Distance_Item;
    public float closingDistance = 1;

    public float Speed;
    private bool Closing = false;

    private Item _item;
    
    private void Awake()
    {
        _item = GetComponent<Item>();
    }

    private void Update()
    {
        Closing = Vector3.Distance(Player.transform.position, transform.position) <= closingDistance;
        
        // if (Closing)
        // {
        //     itemInfo.Init(_item);
        //     if (GetComponent<Transform>().position.x < Cam.position.x - Cam_Distance_Item.x)
        //         GetComponent<Transform>().position = new Vector3(GetComponent<Transform>().position.x + Speed, GetComponent<Transform>().position.y, GetComponent<Transform>().position.z);
        //     if (GetComponent<Transform>().position.x > Cam.position.x - Cam_Distance_Item.x)
        //         GetComponent<Transform>().position = new Vector3(GetComponent<Transform>().position.x - Speed, GetComponent<Transform>().position.y, GetComponent<Transform>().position.z);
        //     if (GetComponent<Transform>().position.y < Cam.position.y - Cam_Distance_Item.y)
        //         GetComponent<Transform>().position = new Vector3(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y + Speed, GetComponent<Transform>().position.z);
        //     if (GetComponent<Transform>().position.x > Cam.position.y - Cam_Distance_Item.y)
        //         GetComponent<Transform>().position = new Vector3(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y - Speed, GetComponent<Transform>().position.z);
        //     if (GetComponent<Transform>().position.z < Cam.position.z - Cam_Distance_Item.z)
        //         GetComponent<Transform>().position = new Vector3(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y, GetComponent<Transform>().position.z + Speed);
        //     if (GetComponent<Transform>().position.z > Cam.position.z - Cam_Distance_Item.z)
        //         GetComponent<Transform>().position = new Vector3(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y, GetComponent<Transform>().position.z - Speed);
        // }
        // else
        // {
        //     itemInfo.ClearInfo(_item);
        // }
    }
}

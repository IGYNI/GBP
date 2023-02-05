using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mr_Move_things : MonoBehaviour
{
    private int i;

    public void Move(Transform obj,
              Vector3 Dest,
              float Speed)
    {
        if (obj.position.x > Dest.x)
            obj.position = new Vector3(obj.position.x - Speed, obj.position.y, obj.position.z);
        if (obj.position.x < Dest.x)
            obj.position = new Vector3(obj.position.x + Speed, obj.position.y, obj.position.z);
        if (obj.position.y < Dest.y)
            obj.position = new Vector3(obj.position.x, obj.position.y + Speed, obj.position.z);
        if (obj.position.y > Dest.y)
            obj.position = new Vector3(obj.position.x, obj.position.y - Speed, obj.position.z);
        if (obj.position.z > Dest.z)
            obj.position = new Vector3(obj.position.x, obj.position.y, obj.position.z - Speed);
        if (obj.position.z < Dest.z)
            obj.position = new Vector3(obj.position.x, obj.position.y, obj.position.z + Speed);
    }

    public void Rotate(Transform obj,
                       Vector3 Dest,
                       float Speed)
    {
        if (obj.rotation.x > Dest.x)
            obj.rotation = Quaternion.Euler(obj.rotation.x - Speed, obj.rotation.y, obj.rotation.z);
        if (obj.rotation.x < Dest.x)
            obj.rotation = Quaternion.Euler(obj.rotation.x + Speed, obj.rotation.y, obj.rotation.z);
        if (obj.rotation.y > Dest.y)
            obj.rotation = Quaternion.Euler(obj.rotation.x, obj.rotation.y - Speed, obj.rotation.z);
        if (obj.rotation.x < Dest.x)
            obj.rotation = Quaternion.Euler(obj.rotation.x, obj.rotation.y + Speed, obj.rotation.z);
        if (obj.rotation.z > Dest.z)
            obj.rotation = Quaternion.Euler(obj.rotation.x, obj.rotation.y, obj.rotation.z - Speed);
        if (obj.rotation.z < Dest.z)
            obj.rotation = Quaternion.Euler(obj.rotation.x, obj.rotation.y, obj.rotation.z + Speed);
        
    }
}

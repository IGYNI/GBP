using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRIGGER_BOXES : MonoBehaviour
{
   
    public float Box_Width; // Ширина выделения Обьекта 

    private box BOX;

    private Camera Cam; 
    private box _current;

    void Awake()
    {
        Cam = GetComponent<Camera>();
    }

    void Update()
    {
        Ray mRay = Cam.ScreenPointToRay(Input.mousePosition); 
        RaycastHit hitInfo;
        if (Physics.Raycast(mRay, out hitInfo))
        {
            var Box = hitInfo.transform.GetComponent<box>();
            if (hitInfo.transform.GetComponent<box>() && hitInfo.transform.GetComponent<box>().IsOnPosition == false)
            {
                _current = Box;
                _current.Box_Outline.OutlineWidth = Box_Width;
            }
        
            else if (Box != _current)
            {
                _current.Box_Outline.OutlineWidth = 0;
                _current = null;
            }
        }
        

    }
}

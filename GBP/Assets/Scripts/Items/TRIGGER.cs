using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class TRIGGER : MonoBehaviour
{
    //private Outline Item; // Обьект который будет выделен !! ОБЬЕКТ ДОЛЖЕН ИМЕТЬ СКРИПТ " Outline " !!
    public float Item_Width; // Ширина выделения Обьекта 
    public UIItemInfo itemInfo;

    private Camera Cam;
    private Item _current;

    void Awake()
    {
        Cam = GetComponent<Camera>();
        //Item = GetComponent<Outline>();    
    }

    void Update ()
    {
        Ray mRay = Cam.ScreenPointToRay(Input.mousePosition); 
        RaycastHit hitInfo;
        // if (Physics.Raycast(mRay, out hitInfo) && hitInfo.transform.gameObject == GetComponent<Transform>().gameObject) // Проверяем попали ли мы и являеться ли платформа той, что мы указали
        //     Item.OutlineWidth = Item_Width;
        // else 
        //     Item.OutlineWidth = 0;
        
        if (Physics.Raycast(mRay, out hitInfo))
        {
            
            var item = hitInfo.transform.GetComponent<Item>(); 
            if (item != null)
            {
                if (item != _current)
                {
                    if (_current != null)
                    {
                        if (_current.interaction != null)
                        {
                            _current.interaction.outline.OutlineWidth = 0;
                        }
                        itemInfo.ClearInfo(_current);
                    }
                    _current = item;
                    itemInfo.Init(_current);
                }

                if (_current.interaction != null)
                {
                    _current.interaction.outline.OutlineWidth = Item_Width;
                }
            }
            else if (_current != null)
            {
                if (_current.interaction != null)
                {
                    _current.interaction.outline.OutlineWidth = 0;
                }
                itemInfo.ClearInfo(_current);
                _current = null;
            }
        }
    }

    

    
}
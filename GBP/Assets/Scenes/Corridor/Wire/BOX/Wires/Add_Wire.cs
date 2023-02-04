using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Add_Wire : MonoBehaviour
{
    public GameObject _currentWIRE;
    public GameObject _lastWIRE;
    private bool CanCreate;

    void Update()
    {
        Debug.Log("sdf");
        Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition); 
        RaycastHit hitInfo;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(mRay, out hitInfo) && hitInfo.transform.GetComponent<Wire_Add_Info>())
            {
                if (_currentWIRE == null)
                {
                    _currentWIRE = hitInfo.transform.gameObject;
                    CanCreate = true;
                }
                
            }
            if (CanCreate && _currentWIRE.GetComponent<Wire_Add_Info>().IsBorned == false)
            {
                _lastWIRE = _currentWIRE;
                
                _currentWIRE.GetComponent<Wire_Add_Info>().CreateWire();
            }
            
        }

    }
}

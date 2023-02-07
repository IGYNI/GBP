using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Add_Wire : MonoBehaviour
{
    public bool IsWin;
    public GameObject[] ListWires;
    public GameObject _currentWIRE;
    public GameObject _lastWIRE;
    private bool CanCreate;
    public List<GameObject> Wires = new List<GameObject>();

    public GameObject _currentTEMP;


    public void Clearr()
    {
        for (int i = 0; i < Wires.Count; i++)
        {
            _currentTEMP = Wires[i];
            Destroy(_currentTEMP);
        }
    }

    void Update()
    {
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
                Wires.Add(_currentWIRE);
            }
            
        }

    }
    void OnDestroy() 
    {
        _currentWIRE = _lastWIRE;   
        _currentWIRE.GetComponent<Wire_Add_Info>().IsBorned = false;
    }
}

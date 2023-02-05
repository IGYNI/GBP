using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire_Add_Info : MonoBehaviour
{
    public GameObject Wire;
    public Transform Anchor;
    public Vector3 Pos_New_Wire;
    
    public bool IsBorned = false;
    private GameObject New_Wire;

    private int i; // без него(р) не работает выключения/включения хз почему
    private void Update() {
        i++;
        i--;
    }

    public void CreateWire()
    {
        Transform _current;
        _current = GetComponent<Transform>();
        Pos_New_Wire = new Vector3( Anchor.position.x, Anchor.position.y , Anchor.position.z);
        New_Wire = Instantiate(Wire, Pos_New_Wire, Quaternion.Euler(0, 0 ,90));
        New_Wire.GetComponent<Wire_Add_Info>().enabled = true; 
        Camera.main.transform.GetComponent<Move_Wire>()._currentWireCam = New_Wire.transform;
        //GetComponent<Wire_Add_Info>().enabled = true;
        IsBorned = true;
        Camera.main.GetComponent<Add_Wire>()._currentWIRE = New_Wire;
    }
}

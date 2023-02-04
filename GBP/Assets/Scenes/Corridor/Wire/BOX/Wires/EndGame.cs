using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) {
        Camera.main.GetComponent<Add_Wire>()._currentWIRE = Camera.main.GetComponent<Add_Wire>()._lastWIRE;
        Camera.main.GetComponent<Move_Wire>()._currentWireCam = Camera.main.GetComponent<Add_Wire>()._currentWIRE.transform;
        Camera.main.GetComponent<Add_Wire>()._lastWIRE.GetComponent<Wire_Add_Info>().IsBorned = true;
        Destroy(other.GetComponent<Collider>().gameObject);
    }

    

}

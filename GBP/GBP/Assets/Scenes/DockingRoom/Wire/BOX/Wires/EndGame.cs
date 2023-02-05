using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public int i;
    public GameObject Start;
    private void OnTriggerEnter(Collider other) 
    {   
        i++;
        if (other.transform.GetComponent<Wire_Add_Info>().IsBorned == false || Input.GetKeyDown("q"))
        {
            Debug.Log("вышел");
            Camera.main.GetComponent<Add_Wire>().Clearr();
            Camera.main.GetComponent<Change_Pos_Camera>().IsOnY = false;
            Camera.main.GetComponent<Change_Pos_Camera>().IsMove = false;
            Camera.main.GetComponent<OpenOrClose>().IsClose = true;
            Start.GetComponent<Wire_Add_Info>().IsBorned = false;

            //Camera.main.GetComponent<Add_Wire>()._currentWIRE.GetComponent<Wire_Add_Info>().IsBorned = false;
            //Camera.main.GetComponent<Add_Wire>()._currentWIRE = null;
            
        }
    }

    

}

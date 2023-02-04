using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cumplete : MonoBehaviour
{
    public bool WIN; 
    public Animation Box; 
    private void OnTriggerEnter(Collider other) 
    {
        
        WIN = true;
        Camera.main.GetComponent<Add_Wire>().enabled = false;
        Camera.main.GetComponent<Move_Wire>().enabled = false;
        Camera.main.GetComponent<Move_Camera>().enabled = false;
        Camera.main.GetComponent<EndGame>().enabled = false;
        Camera.main.GetComponent<Change_Pos_Camera>().enabled = false;
        Box["close"].layer = 1;
        Box.Play("close");

    }
}

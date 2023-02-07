using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cumplete : MonoBehaviour
{
    public bool WIN; 
    public GameObject Box;   
    private int i;
    private void OnTriggerEnter(Collider other) 
    {
        i++;
        if (i >= 2)
        {

            WIN = true;
            Camera.main.GetComponent<Add_Wire>().Clearr();
            Camera.main.GetComponent<OpenOrClose>().IsClose = true;
        
        }

    }
}

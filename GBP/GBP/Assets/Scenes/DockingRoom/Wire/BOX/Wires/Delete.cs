using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour
{
    private void Start() 
    {
        GetComponent<Animation>().Play("Open");    
    }
}

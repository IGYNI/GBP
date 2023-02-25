using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class RotatePipe: MonoBehaviour
{
    [SerializeField] private int state;
    [SerializeField] private int[] puzzleState;
    [SerializeField] private bool active;


    public bool Active
    {
        get { return active; }
    }
    public void Rotate()
    {
        //transform.eulerAngles += new Vector3(0, 0, 90);
        transform.Rotate(0, 0, 90);
        state++;
        if(state > 3)
        {
            state = 0;
        }
    }
    public bool IsMatch()
    {
        for(int i = 0; i < puzzleState.Length; i++)
        {
            if(state == puzzleState[i])
            {
                return true;
            }
        }
        return false;
    }
    //private void OnValidate()
    //{
    //    var z = transform.localEulerAngles.z;

    //    if (z == 0) state = 0;
    //    if (z == 90) state = 1;
    //    if (z == 180) state = 2;
    //    if (z == 270) state = 3;
    //}
}


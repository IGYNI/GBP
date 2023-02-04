using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Wire : MonoBehaviour
{
    public Transform _currentWireCam;
    private Vector3 MousePos;
    private bool Y;

    void Update()
    {
        MousePos = Input.mousePosition;
        Y = GetComponent<Change_Pos_Camera>().IsOnY;
        if (Y == false)
        {
            try
            {
                _currentWireCam.rotation = Quaternion.Euler(_currentWireCam.rotation.x, _currentWireCam.rotation.y, MousePos.y / 3.7f - 200);
            }
            catch (UnassignedReferenceException)
            {

                Debug.Log("Лошара");
            }
        }
        else if (Y)
        {
            try
            {
                if (GetComponent<Change_Pos_Camera>().IsRotate == false)
                    _currentWireCam.rotation = Quaternion.Euler(MousePos.y / 4 - 160,_currentWireCam.rotation.y , _currentWireCam.rotation.z );
                else if (GetComponent<Change_Pos_Camera>().IsRotate)
                    _currentWireCam.rotation = Quaternion.Euler((MousePos.y / 4 - 160) * -1,_currentWireCam.rotation.y , _currentWireCam.rotation.z );
            }
            catch (UnassignedReferenceException)
            {

                Debug.Log("Лошара");
            }
        }
        
    }
}

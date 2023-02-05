using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenOrClose : MonoBehaviour
{
    public Vector3 First_pos;
    public Quaternion First_rot;
    public bool StartPos;
    public GameObject Player;
    private GameObject _currentBox;
    
    public bool IsClose;//я хз как передать с одного скрипта в другой енумку

    public bool IsWin;
    public enum Stat
    {
        Open,
        Nothing,
        In,
        Close
    }

    public Stat _currentStat;

    void Awake() {
        _currentStat = Stat.Nothing;    
    }

    void Update()
    {
        if (IsWin == false)
        {
            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition); 
            RaycastHit hitInfo;
            if (Physics.Raycast(mRay, out hitInfo) && hitInfo.transform.GetComponent<BoxInfo>())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _currentStat = Stat.Open;
                    _currentBox = hitInfo.transform.gameObject;

                }
            }
            if (IsClose)
            {
                IsClose = false;
                _currentStat = Stat.Close;
            }
            if (_currentStat == Stat.Open)
            {
                    Player.GetComponent<PlayerController>().enabled = false;
                    Camera.main.transform.rotation = Quaternion.Euler(_currentBox.transform.GetComponent<BoxInfo>().Rot.x, _currentBox.transform.GetComponent<BoxInfo>().Rot.y, _currentBox.transform.GetComponent<BoxInfo>().Rot.z);
                    Camera.main.transform.position = _currentBox.transform.GetComponent<BoxInfo>().Pos;

                    _currentBox.GetComponent<Collider>().enabled = false;

                    Camera.main.transform.GetComponent<Change_Pos_Camera>().enabled = true;
                    Camera.main.GetComponent<Move_Camera>().enabled = true;
                    Camera.main.GetComponent<Move_Wire>().enabled = true;
                    Camera.main.GetComponent<Add_Wire >().enabled = true;

                    _currentStat = Stat.In;
            }
            else if (_currentStat == Stat.Close)
            {

                    Camera.main.transform.GetComponent<Change_Pos_Camera>().enabled = false;
                    Camera.main.GetComponent<Move_Camera>().enabled = false;
                    Camera.main.GetComponent<Move_Wire>().enabled = false;
                    Camera.main.GetComponent<Add_Wire >().enabled = false;
                    Camera.main.transform.position = First_pos;
                    Camera.main.transform.rotation = First_rot;
                    _currentStat = Stat.Nothing;

                    _currentBox.GetComponent<Collider>().enabled = true;
                    Player.GetComponent<PlayerController>().enabled = true;
            }
            else if (_currentStat == Stat.Nothing)
            {
                First_pos = GetComponent<Transform>().position;
                First_rot = Quaternion.Euler(Camera.main.transform.localEulerAngles.x,Camera.main.transform.localEulerAngles.y, Camera.main.transform.localEulerAngles.z );
            }
        }
        

    }   
}

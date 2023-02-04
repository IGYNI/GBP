using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Is_Start_Puzzle_wire : MonoBehaviour
{
    public bool StartGo;
    public float Speed;
    void Update()
    {
        Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition); 
        RaycastHit hitInfo;
        if (Physics.Raycast(mRay, out hitInfo) && hitInfo.transform.gameObject.tag == "Box")// сергей прости, у меня мозг плавиться и немогу шот придумать по лучше, соре
        {
            if (Input.GetMouseButtonDown(0))
            {
                Camera.main.GetComponent<Add_Wire>().enabled = true;
                Camera.main.GetComponent<Change_Pos_Camera>().enabled = true;
                Camera.main.GetComponent<Move_Camera>().enabled = true;
                Camera.main.GetComponent<Move_Wire>().enabled = true;
                hitInfo.transform.GetComponent<Collider>().enabled = false;
                StartGo = true;
                
            }
                
        }
        if (StartGo)
        {
            Camera.main.GetComponent<Mr_Move_things>().Move(Camera.main.transform, new Vector3(-2.8f, 1.5f, -9.05f), Speed);
            Camera.main.transform.rotation = Quaternion.Euler(10.2f, -90.12f, 0);
            if (Camera.main.transform.position == new Vector3(-2.8f, 1.3f, -9.05f))
                StartGo = false;
        }
    }
}

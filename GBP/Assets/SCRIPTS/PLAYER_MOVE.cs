using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PLAYER_MOVE : MonoBehaviour
{
    
    public Camera Cam; // Камера из которой будет сробатывать лазер
    public int MouseButton; // На какую кнопку нажато (0 - левая кнопка мышы, 1 - правая кнопка мышы)

    void FixedUpdate() // Функция работает в 60fps без разницы сколько на компютере в данный момент fps
    {
        Ray mRay = Cam.ScreenPointToRay(Input.mousePosition); 
        RaycastHit hitInfo;
        if (Input.GetMouseButton(MouseButton)) // Если кнопка 
        {
            if (Physics.Raycast(mRay, out hitInfo, 1000) && hitInfo.transform.CompareTag("FLOOR")) // Проверяем попали ли мы и есть ли у обьекта тэг "FLOOR"
            {
                GetComponent<NavMeshAgent>().destination = hitInfo.point; // Ставим конечную позицию обьекта там где мы попали лазером который выстрелил из позиции на екране и позиции камеры
            }
        }
        
    }
}

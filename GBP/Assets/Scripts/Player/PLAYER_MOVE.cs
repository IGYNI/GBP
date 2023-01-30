using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PLAYER_MOVE : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Walk,
        FinishWalk,
        Run
    }

    //public short OnMovePlayer; // 0 стоит | 1 идет | 2 пришел
    public PlayerState State;
    private PlayerState _previousState;
    private bool CanTakePlayer;
    private Vector3 LastPosition;

    public Camera Cam; // Камера из которой будет сробатывать лазер
    public int MouseButton; // На какую кнопку нажато (0 - левая кнопка мышы, 1 - правая кнопка мышы)

    public GameObject[] Items;
    public GameObject Platform_For_Walk;

    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void SetDestination(Vector3 destination)
    {
        _navMeshAgent.destination = destination;
        _navMeshAgent.updateRotation = true;
    }

    private void Update() // Функция работает в 60fps без разницы сколько на компютере в данный момент fps
    {
        Ray mRay = Cam.ScreenPointToRay(Input.mousePosition); 
        RaycastHit hitInfo;
        if (Input.GetMouseButton(MouseButton)) // Если кнопка нажата
        {
            if (Physics.Raycast(mRay, out hitInfo)) // Проверяем попали ли мы и являеться ли платформа той, что мы указали
            {
                if (hitInfo.transform.gameObject == Platform_For_Walk)
                {
                    SetDestination(hitInfo.point);
                }
                // else 
                // {
                //     for (int i = 0; i < Items.Length; i++)
                //     {
                //         if (hitInfo.transform.gameObject == Items[i])
                //         {
                //             _navMeshAgent.destination = hitInfo.point; // Ставим конечную позицию обьекта там где мы попали лазером который выстрелил из позиции на екране и позиции камеры
                //             if (OnMovePlayer == 0)
                //                 OnMovePlayer =  1;
                //         }
                //     }
                // }
            }
        }
        
        // if (OnMovePlayer == 1 && _navMeshAgent.remainingDistance < 0.3f  && _navMeshAgent.remainingDistance > 0.01f )
        //     OnMovePlayer += 1;
        //
        // if (_navMeshAgent.remainingDistance < 0.3f && OnMovePlayer == 2)
        //     OnMovePlayer = 0;
        //
        // Second_Frame = GetComponent<Transform>().position;
    }

    private void LateUpdate()
    {
        var distance = Vector3.Distance(transform.position, LastPosition);
        if (distance <= 0.01f)
        {
            State = PlayerState.Idle;
        } 
        else if (_navMeshAgent.remainingDistance < 0.3f && _navMeshAgent.remainingDistance > 0.01f)
        {
            State = PlayerState.FinishWalk;
        }
        else
        {
            State = PlayerState.Walk;
        }

        if (_previousState != State)
        {
            Debug.Log(State);
            _previousState = State;
        }
        LastPosition = transform.position;
    }
}
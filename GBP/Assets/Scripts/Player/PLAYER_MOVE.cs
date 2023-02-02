using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FMOD.Studio;
using static UnityEngine.ParticleSystem;

public class PLAYER_MOVE : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Walk,
        FinishWalk,
        Run,
        DrugedBox,
        DrugingBox
    }

    //public short OnMovePlayer; // 0 стоит | 1 идет | 2 пришел
    public PlayerState State;
    private Transform Player_pos;
    private PlayerState _previousState;
    private Vector3 LastPosition;
    private bool CanMove = true;
    private Transform _current_box;
    private box _current_boxclass;
    private short zat; // анальна затычка (на удачу)
    
    public Camera Cam; // Камера из которой будет сробатывать лазер
    public int MouseButton; // На какую кнопку нажато (0 - левая кнопка мышы, 1 - правая кнопка мышы)

    public GameObject Platform_For_Walk;

    private NavMeshAgent _navMeshAgent;

    private EventInstance footsteps;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        Player_pos = GetComponent<Transform>();
        footsteps = AudioManager.instance.CreateInstance(AudioManager.instance.events.footsteps);
    }

    public void SetDestination(Vector3 destination)
    {
        _navMeshAgent.destination = destination;
        _navMeshAgent.updateRotation = true;
    }



    private void DrugBox()
    {
        if (State == PlayerState.FinishWalk && _current_box != null)
        {
            State = PlayerState.DrugingBox;
            _current_box.SetParent(Player_pos) ;
        }
        if (State == PlayerState.DrugingBox)
        {
            SetDestination(_current_box.gameObject.GetComponent<box>().destination);
            CanMove = false;
        }
        if (State == PlayerState.DrugedBox)
        {
            CanMove = true;
            _current_box.parent = null;
            _current_boxclass.IsOnPosition = true;
        }
    } 



    private void Update() // Функция работает в 60fps без разницы сколько на компютере в данный момент fps
    {
        if (Input.GetMouseButton(MouseButton) && CanMove) // Если кнопка нажата
        {
            Ray mRay = Cam.ScreenPointToRay(Input.mousePosition); 
            RaycastHit hitInfo;

            if (Physics.Raycast(mRay, out hitInfo)) // Проверяем попали ли мы и являеться ли платформа той, что мы указали
            {
                if (hitInfo.transform.gameObject == Platform_For_Walk || hitInfo.transform.GetComponent<box>())
                {
                    SetDestination(hitInfo.point);
                    if (hitInfo.transform.GetComponent<box>() && hitInfo.transform.GetComponent<box>().IsOnPosition == false)
                    {
                        var Box = hitInfo.transform;
                        var Boxclass = hitInfo.transform.GetComponent<box>();
                        if (_current_box == null)
                        {
                            _current_box = Box;
                            _current_boxclass = Boxclass;
                        }
                        
                    }
                    else
                        _current_box = null;
                }
               
            }
        }
        UpdateSounds();
        DrugBox();
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
            ;

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
        if (State == PlayerState.FinishWalk && CanMove == false)
        
            State = PlayerState.DrugedBox;
        
        LastPosition = transform.position;
    }

    private void UpdateSounds()
    {
        PLAYBACK_STATE playbackState;
        footsteps.getPlaybackState(out playbackState);
        if(playbackState.Equals(PLAYBACK_STATE.STOPPED))
            if(State== PlayerState.Walk)
        {
            footsteps.start();
        }
        else
        {
            footsteps.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }
}
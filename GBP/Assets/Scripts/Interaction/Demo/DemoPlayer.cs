using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class DemoPlayer : MonoBehaviour
{
    
    [SerializeField] private float speed;
    [SerializeField] private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector3 force = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            force += Vector3.forward * (speed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            force -= Vector3.forward * (speed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            force -= Vector3.right * (speed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            force += Vector3.right * (speed * Time.deltaTime);
        }

        if (force != Vector3.zero)
        {
            characterController.Move(force);
            //rBody.MovePosition(transform.position + );
        }
    }
}

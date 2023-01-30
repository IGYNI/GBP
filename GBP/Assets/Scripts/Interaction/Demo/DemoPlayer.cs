using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DemoPlayer : MonoBehaviour
{
    
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rBody;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody>();
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
            rBody.MovePosition(transform.position + force);
        }
    }
}

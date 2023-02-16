
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotationPuzzle : Puzzle
{
    [SerializeField] private Camera raycastCamera;
    [SerializeField] private LayerMask _layerMask;
    public List<RotatePipe> pipes;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            UpdateRaycast();
        }
        CheckLogic();
    }
    private void UpdateRaycast()
    {
        //if (_blockInput)
        // return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray mRay = raycastCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(mRay.origin, mRay.direction, out RaycastHit hitInfo, 100))
            {
                var pipe = hitInfo.collider.gameObject.GetComponentInParent<RotatePipe>();
                if (pipe != null)
                {
                    
                    pipe.Rotate();                
                }
            }
        }
    }
    private void CheckLogic()
    {
        bool allMatch = true;
        for (int i = 0; i < pipes.Count; i++)
        {
            if (pipes[i].Active)
            {
                if (pipes[i].IsMatch() == false)
                {
                    allMatch = false;
                    break;
                }
            }
        }
        if (allMatch)
        {
            Debug.Log("PuzzleComplete");
        }
    }

}


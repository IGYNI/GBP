using System;
using UnityEngine;

public class WirePart : MonoBehaviour
{
	public Transform mountPoint;
	public Vector3 cursor;

	public bool IsCollide;
	public Collider enterCollider;

	private bool _freeze;

	public void Freeze()
	{
		_freeze = true;
	}

	public void UpdateInput(Camera cam, bool cameraFlip, int layerMask)
	{
		if (_freeze)
			return;
		
		Ray mRay = cam.ScreenPointToRay(Input.mousePosition);
		
		if (Physics.Raycast(mRay.origin, mRay.direction, out RaycastHit hitInfo, 100, layerMask))
		{
			//Debug.Log(hitInfo.transform.name);
			
			cursor = hitInfo.point;
			var direction = cursor - transform.position;
			if (cameraFlip)
			{
				direction.y = 0;
				cursor = (hitInfo.point - transform.position).normalized * transform.position.y;
			}
			else
			{
				direction.z = 0;
			}
			direction.Normalize();
			transform.forward = direction;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out WirePart otherPart))
		{
			//skip
		}
		else
		{
			IsCollide = true;
			enterCollider = other;
			//Debug.Log($"{name}  collider with {other.gameObject.name}");
		}
	}
	
	private void OnTriggerExit(Collider other)
	{
		if (other.TryGetComponent(out WirePart otherPart))
		{
			//skip
		}
		else
		{
			IsCollide = false;
			enterCollider = null;
			//Debug.Log($"{name} Stop colliding");
		}
	}
}

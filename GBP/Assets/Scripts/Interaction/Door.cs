using System.Collections;
using System.Collections.Generic;
using System.Resources;
using DG.Tweening;
using Interaction;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
	public enum EState
	{
		Closed,
		Open,
		Transition,
	}

	public EState state;

	[SerializeField] private Vector3 openedAngle;
	[SerializeField] private Vector3 closedAngle;
	[SerializeField] private GameObject viewHolder;


	[ContextMenu("Open")]
	public void Open()
	{
		if (state == EState.Closed)
		{
			state = EState.Transition;
			viewHolder.transform.DOLocalRotate(openedAngle, 1f).SetEase(Ease.InOutSine).OnComplete(() => state = EState.Open);
		}
	}

	[ContextMenu("Close")]
	public void Close()
	{
		if (state == EState.Open)
		{
			state = EState.Transition;
			viewHolder.transform.DOLocalRotate(closedAngle, 1f).SetEase(Ease.InOutSine).OnComplete(() => state = EState.Closed);
		}
	}

	public void Interact()
	{
		switch (state)
		{
			case EState.Transition:
				return;
			case EState.Closed:
				Open();
				break;
			case EState.Open:
				Close();
				break;
		}
	}
}
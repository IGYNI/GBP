using System;
using System.Collections.Generic;
using DG.Tweening;
using Interaction;
using UnityEngine;

public class AutomaticDoor : MonoBehaviour, IInteractable
{
	[Serializable]
	public class Part
	{
		public Transform opened;
		public Transform closed;
		public GameObject view;
	}
	
	public enum EState
	{
		Closed,
		Open,
		Transition,
	}
	
	public EState state;
	[SerializeField] private List<Part> doorParts;
	[SerializeField] private float duration = 1f;
	
	[ContextMenu("Open")]
	public void Open()
	{
		if (state == EState.Closed)
		{
			state = EState.Transition;
			foreach (Part doorPart in doorParts)
			{
				doorPart.view.transform.DOMove(doorPart.opened.transform.position, duration).SetEase(Ease.InOutSine).OnComplete(() => state = EState.Open);
				doorPart.view.transform.DORotate(doorPart.opened.transform.eulerAngles, duration).SetEase(Ease.InOutSine);
			}
		}
	}

	[ContextMenu("Close")]
	public void Close()
	{
		if (state == EState.Open)
		{
			state = EState.Transition;
			foreach (Part doorPart in doorParts)
			{
				doorPart.view.transform.DOMove(doorPart.closed.transform.position, duration).SetEase(Ease.InOutSine).OnComplete(() => state = EState.Closed);
				doorPart.view.transform.DORotate(doorPart.closed.transform.eulerAngles, duration).SetEase(Ease.InOutSine);
			}
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
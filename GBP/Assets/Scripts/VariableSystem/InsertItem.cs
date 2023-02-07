using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class InsertItem : InteractionSequence
{
	[SerializeField] private GameObject view;
	[SerializeField] private Transform origin;
	[SerializeField] private Transform target;
	[SerializeField] private float sequenceTime = 1f;
	
	public override IEnumerator Proceed()
	{
		view.transform.position = origin.transform.position;
		view.transform.rotation = origin.transform.rotation;
		view.SetActive(true);
		view.transform.DORotate(target.transform.eulerAngles, sequenceTime);
		var tween = view.transform.DOMove(target.transform.position, sequenceTime).SetEase(Ease.Linear);
		yield return tween.WaitForCompletion();
		view.SetActive(false);
	}
}

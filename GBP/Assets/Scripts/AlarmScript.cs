using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AlarmScript : MonoBehaviour
{
	[SerializeField] private float speed;
	[SerializeField] private Light _lightR;
	[SerializeField] private Light _lightL;
	[SerializeField] private bool _alarm;
	[SerializeField] private bool playOnStart;


	public bool Alarm
	{
		get => _alarm;
		set => _alarm = value;
	}

	private void Start()
	{
		if (playOnStart)
			Alarm = true;
	}

	// Update is called once per frame
	private void Update()
	{
		switch (_alarm)
		{
			case true:
				Play();
				break;
			case false:
				Stop();
				break;
		}
	}

	public void Play()
	{
		_lightR.enabled = true;
		_lightL.enabled = true;
		_lightL.transform.Rotate(0, speed * Time.deltaTime, 0);
		_lightR.transform.Rotate(0, speed * Time.deltaTime, 0);
	}

	public void Stop()
	{
		_lightR.enabled = false;
		_lightL.enabled = false;
		_lightR.transform.Rotate(0, 0, 0);
		_lightL.transform.Rotate(0, 0, 0);
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmScript : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] Light _lightR;
    [SerializeField] Light _lightL;
    [SerializeField] bool _alarm;

    public bool _Alarm
    {
        get { return _alarm; }
        set { _alarm = value; }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_alarm)
        {
            Play();
        }
        else if (!_alarm)
        {
            Stop();
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

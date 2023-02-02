using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioChanger : MonoBehaviour
{
    [Header("Area")]
    [SerializeField] private MusicArea area;

    private void Awake()
    {
        AudioManager.instance.SetMusicArea(area);
        Debug.Log(area);
    }
}

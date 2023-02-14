using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToAudioTheme : MonoBehaviour
{
    [Header("Area")]
    [SerializeField] private MusicArea area;
    
    
    public void Switch()
    {
        AudioManager.instance.SetMusicArea(area);
        Debug.Log($"[AudioChanger] Set music area {area}");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private string _sceneName;
    private EventInstance mainThemeInstance;
    private EventInstance ambience;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Audio Manager in scene");
        }
        instance = this; 
        DontDestroyOnLoad(gameObject);
        
    }

    private void Start()
    {

        InitializeAmbience(FMODEvents.instance.ambience);

    }

    public void PlayOneShot(EventReference sound, Vector3 worldPosition)
    {
        RuntimeManager.PlayOneShot(sound, worldPosition);
    }

    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }

    public void InitializeAmbience(EventReference ambienceEventReference)
    {
        ambience = CreateInstance(ambienceEventReference);
        ambience.start();
    }

    public void InitializeMainTheme(EventReference mainThemeEventReference)
    {
        mainThemeInstance = CreateInstance(mainThemeEventReference);
        mainThemeInstance.start();
    }

    public void SetMusicArea(MusicArea area)
    {
        ambience.setParameterByName("AMB", (float)area);
    }

}

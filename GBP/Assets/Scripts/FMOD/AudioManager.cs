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
    private EventInstance footsteps;

    private EventInstance mainThemeInstance;

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
        InitializeMainTheme(FMODEvents.instance.mainTheme);
        _sceneName = SceneManager.GetActiveScene().name;
        footsteps = instance.CreateInstance(FMODEvents.instance.footsteps);
    }

    private void Update()
    {
        SceneFootSteps();
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

    private void InitializeMainTheme(EventReference mainThemeEventReference)
    {
        mainThemeInstance = CreateInstance(mainThemeEventReference);
        //mainThemeInstance.start();
    }

    private void SetMusicArea(MusicArea area)
    {
        mainThemeInstance.setParameterByName("area", (float) area);
    }

    private void SceneFootSteps()
    {
        if (_sceneName == "Corridor")
        {
            footsteps.setParameterByName("footsteps", 1);
        }
        else
        {
            footsteps.setParameterByName("footsteps", 0);
        }
    }
}

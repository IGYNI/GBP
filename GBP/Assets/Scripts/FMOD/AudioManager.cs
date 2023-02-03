using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public EventInstance PlayerFootsteps { get; private set; }

    public FMODEvents events;
    [SerializeField] private StudioBankLoader bankLoaderPrefab;
    private string _sceneName;
    private EventInstance ost;
    private EventInstance ambience;
    private EventInstance buttonHandler;
    private EventInstance buttonClick;
    private StudioBankLoader _bank;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            _bank = Instantiate(bankLoaderPrefab, transform);
            InitializePlayerFootsteps();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        
        // if (instance != null)
        // {
        //     Debug.LogError("Found more than one Audio Manager in scene");
        // }
        // instance = this; 
        // DontDestroyOnLoad(gameObject);
    }
    
    private void OnDestroy()
    {
        if (instance == this)
        {
            Debug.Log($"Clear instance OnDestroy {gameObject.name}");
            instance = null;
        }
    }

    private void OnApplicationQuit()
    {
        Debug.Log($"Clear instance OnApplicationQuit {gameObject.name}");
        instance = null;
    }

    private void Start()
    {
        
        InitializeAmbience(events.ambience);
        InitializeOst(events.mainTheme);

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

    public void InitializeOst(EventReference ostReference)
    {
        ost = CreateInstance(ostReference);
        ost.start();
    }

    private void InitializePlayerFootsteps()
    {
        PlayerFootsteps = CreateInstance(events.footsteps);
    }

    public void SetMusicArea(MusicArea area)
    {
        ambience.setParameterByName("AMBIENCE", (float)area);
        ost.setParameterByName("MusicArea", (float)area);
    }

    public void InitializeMenuButtonHandler() 
    {
        buttonHandler = CreateInstance(events.buttonHover);
        buttonHandler.start();
    }

    public void InitializeMenuButtonClick()
    {
        buttonClick = CreateInstance(events.buttonClick);
        buttonClick.start();
    }

}

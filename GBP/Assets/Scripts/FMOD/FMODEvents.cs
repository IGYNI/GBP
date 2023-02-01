using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [field: Header("SFX")]
    [field: Header("footsteps")]
    [field: SerializeField] public EventReference footsteps { get; private set; }
    [field: Header("Doors Open")]
    [field: SerializeField] public EventReference openDoors { get; private set; }
    [field: Header("Doors Closed")]
    [field: SerializeField] public EventReference closeDoors { get; private set; }

    [field: Header("Music")]
    [field: Header("Main Theme")]
    [field: SerializeField] public EventReference mainTheme { get; private set; }


    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMODEvents instance in scene");
        }
        instance = this;
    }
}

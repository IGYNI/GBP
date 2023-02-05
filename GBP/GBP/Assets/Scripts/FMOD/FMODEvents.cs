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
    [field: Header("Grab Item")]
    [field: SerializeField] public EventReference grabItem { get; private set; }
    [field: Header("UI_hover")]
    [field: SerializeField] public EventReference buttonHover { get; private set; }
    [field: Header("New Game Click")]
    [field: SerializeField] public EventReference buttonClick { get; private set; }
    [field: Header("Menu Buttons Click")]
    [field: SerializeField] public EventReference buttonsClick { get; private set; }
    [field: Header("lock Buttons Click")]
    [field: SerializeField] public EventReference lockButtonsClick { get; private set; }
    [field: Header("Lock Fail")]
    [field: SerializeField] public EventReference lockFail { get; private set; }
    [field: Header("Lock Done")]
    [field: SerializeField] public EventReference lockDone { get; private set; }
    [field: Header("Lock Open")]
    [field: SerializeField] public EventReference lockOpen { get; private set; }
    [field: Header("Fabricator Anim")]
    [field: SerializeField] public EventReference fabricatorAnim { get; private set; }
    [field: Header("Extinguish")]
    [field: SerializeField] public EventReference extinguish { get; private set; }
    [field: Header("Root Growth")]
    [field: SerializeField] public EventReference rootGrowth { get; private set; }
    [field: Header("Ambience")]
    [field: SerializeField] public EventReference ambience { get; private set; }
    [field: Header("Music")]
    [field: SerializeField] public EventReference mainTheme { get; private set; }


    // public static FMODEvents instance { get; private set; }
    //
    // private void Awake()
    // {
    //     if (instance != null)
    //     {
    //         Debug.LogError("Found more than one FMODEvents instance in scene");
    //     }
    //     instance = this;
    // }
}

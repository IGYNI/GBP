using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
   
    public void NewGameButtonClick()
    {
       AudioManager.instance.InitializeMenuButtonClick();
    }
    public void ButtonsClick()
    {
        AudioManager.instance.InitializeMenuClick();
    }
}

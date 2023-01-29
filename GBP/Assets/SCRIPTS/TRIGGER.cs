using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRIGGER : MonoBehaviour
{
    public Outline Item; // Обьект который будет выделен !! ОБЬЕКТ ДОЛЖЕН ИМЕТЬ СКРИПТ " Outline " !!
    public float Item_Width; // Ширина выделения Обьекта 

    void OnTriggerEnter(Collider other) // Если игрок ввошел в тригер !! ИГРОК ДОЛЖЕН ИМЕТЬ Rigidbody !!
    {
        Item.OutlineWidth = Item_Width; // Присваиваем обьекту ширину которая указанна в переменной Item_Width
    }
    void OnTriggerExit(Collider other) // Если игрок ввышел из тригера !! ИГРОК ДОЛЖЕН ИМЕТЬ Rigidbody !!
    {
        Item.OutlineWidth = 0; // Присваиваем обьекту ширину значению 0, дабы создать эфект выключения выделения 
    }

    
}

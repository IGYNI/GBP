using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CodeLock : MonoBehaviour
{
    public TextMesh textCode;
    static string textCodeValue = "";


    private void Start()
    {

    }
    void Update()
    {
        textCode.text = textCodeValue;

    }
    public static void Number(string num)
    {
        if (textCodeValue.Length < 4) textCodeValue += num;
    }
    public static void DeNumber()
    {
        if(textCodeValue.Length > 0)
        {
             textCodeValue = textCodeValue.Remove(textCodeValue.Length - 1);
        }
    }
    public static void Enter()
    {
        if (textCodeValue == "1234")
        {
            Debug.Log("Верно");
            textCodeValue = "";
        }
        else
        {
            textCodeValue = "";
            Debug.Log("Не верно");
        }
    }
    public void Error()
    {
        textCodeValue = ""; 
        Debug.Log("Не верно");
    }
}

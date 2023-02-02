using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickButton : MonoBehaviour
{
    [SerializeField] public CodeLock codeLoke;
    [SerializeField] private string num;

    public string Num()
    {
        return num;
    }
    private void OnMouseDown()
    {
        switch (Num()) 
        {
            case "1":
            case "2":
            case "3":
            case "4":
            case "5":
            case "6":
            case "7":
            case "8":
            case "9":
            case "0":
                CodeLock.Number(Num());
                break;
            case "Enter":
                CodeLock.Enter();
                break;
            case "DeNumber":
                CodeLock.DeNumber();
                break;


        }
    }

}

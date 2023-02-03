using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScriptJournal : MonoBehaviour
{
    public static string journalText;
    [SerializeField] TextMeshProUGUI journalTextValue;
    void Start()
    {
        journalText = "";
    }

    public static void SplitText(string text)
    {
        journalText += text + "\n\n";
    }

    void Update()
    {
        journalTextValue.text = journalText;

        if (Input.GetMouseButtonDown(1))
        {
            SplitText("Запис щоденника №128 Проект: GHA-47t Старший науковий співробітник: Засекречено. Сьогодні нам вдалося синтезувати молекули білка сумісні з піддослідним зразком. Із завтрашнього дня ми вже зможемо розпочати виготовлення прототипу сироватки. Після стільки днів невдач я вже починав сумніватися в успіху проекту, але тепер усе змінилося. Наразі питання збільшення фінансування – порожня формальність.");
        }

    }
}

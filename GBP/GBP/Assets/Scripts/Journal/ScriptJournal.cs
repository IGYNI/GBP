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
            SplitText("����� ��������� �128 ������: GHA-47t ������� �������� ����������: �����������. ������� ��� ������� ����������� �������� ���� ����� � ���������� �������. �� ������������ ��� �� ��� ������� ��������� ������������ ��������� ���������. ϳ��� ������ ��� ������ � ��� ������� ���������� � ����� �������, ��� ����� ��� ��������. ����� ������� ��������� ������������ � ������� �����������.");
        }

    }
}

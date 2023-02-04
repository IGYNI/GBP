using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightbulbScript : MonoBehaviour
{
    [SerializeField] private GameObject lightRed;
    [SerializeField] private GameObject lightGreen;
    [SerializeField] private bool lightSwich;

    void Start()
    {
        StartCoroutine(Flickering());
        lightRed.SetActive(true);
        lightGreen.SetActive(false);
    }

    void Update()
    {

    }

    private IEnumerator Flickering()
    {
        while (lightSwich)
        {
            lightRed.SetActive(true);
            lightGreen.SetActive(false);
            yield return new WaitForSecondsRealtime(2f);

            lightRed.SetActive(false);
            lightGreen.SetActive(true);
            yield return new WaitForSecondsRealtime(2f);
        }

    }

}

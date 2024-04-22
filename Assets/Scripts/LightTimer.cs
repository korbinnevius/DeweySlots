using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTimer : MonoBehaviour
{

    public GameObject LightGroup1;
    public GameObject LightGroup2;
    public float DelayTimer = 0f;

    private void Start()
    {
        LightGroup1.SetActive(false);
        LightGroup2.SetActive(true);
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LightGroup1.SetActive(false);
            LightGroup2.SetActive(true);
            Invoke("EnableLights", DelayTimer);
            Invoke("DisableLights", DelayTimer);
        }
    }

    void EnableLights()
    {
        LightGroup1.SetActive(true);
        Invoke("EnableLightsAgain", DelayTimer);
    } 
    void DisableLights()
    {
        LightGroup2.SetActive(false);
        Invoke("DisableLightAgain", DelayTimer);
    }

    void EnableLightsAgain()
    {
        LightGroup1.SetActive(false);
        Invoke("EnableLights", DelayTimer);
    }
    void DisableLightAgain()
    {
        LightGroup2.SetActive(true);
        Invoke("DisableLights", DelayTimer);
    }
}

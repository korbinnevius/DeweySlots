using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightGroupChanger : MonoBehaviour
{

    public GameObject AfterResults;
    public GameObject BeforeResults;
    public float DelayTimer = 0f;

    private void Start()
    {
        AfterResults.SetActive(false);
        BeforeResults.SetActive(true);
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AfterResults.SetActive(false);
            BeforeResults.SetActive(true);
            Invoke("EnableLightGroup", DelayTimer);
            Invoke("DisableLightGroup", DelayTimer);
        }
    }

    void EnableLightGroup()
    {
        AfterResults.SetActive(true);
    } 
    void DisableLightGroup()
    {
        BeforeResults.SetActive(false);
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;

public class TextboxEnable : MonoBehaviour
{

    public GameObject TextBox;
    public float DelayTimer = 0f;

    private void Start()
    {
        TextBox.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TextBox.SetActive(false);
            Invoke("EnableTextBox", DelayTimer); 
        } 
    }

    void EnableTextBox()
    {
        TextBox.SetActive(true);
    }
}

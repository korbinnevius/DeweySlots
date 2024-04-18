using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArduinoButtonTest : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ArduinoPress();
    }

    void ArduinoPress()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.Translate(Vector3.right * 2f);
        }
    }
}

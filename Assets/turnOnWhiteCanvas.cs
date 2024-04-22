using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnOnWhiteCanvas : MonoBehaviour
{
    [SerializeField] private GameObject whiteCanvas;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        TurnOnWhiteCanvas();   
    }

    public void TurnOnWhiteCanvas()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            whiteCanvas.SetActive(true);
        }
    }
}

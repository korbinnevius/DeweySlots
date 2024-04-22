using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class StartSpinAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool spinning;

    public GetRandomBook GetRandomBook;
    public TextboxEnable TextboxEnable;
    public LightTimer LightTimer;
    public LightGroupChanger LightGroupChanger;
    public turnOnWhiteCanvas TurnOnWhiteCanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        //animator.SetBool("Spinning", false);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        StartSpinning();
    }

    void StartSpinning()
    {
        // %% !spinning is checking to see i f
        if (Input.GetKeyDown(KeyCode.Space) && !spinning )
        {
            //Clear Text before starting animation
            
            //disable box
            //On Button Press starting Spinning animation 
            TextboxEnable.EnableTextBox(false);
            animator.SetTrigger("Spinning");
            spinning = true;
        }
    }

    public void SpinningStopped()
    {
       
        //When the Spinning has stopped
        spinning = false;
        GetRandomBook.ClearSlotsText();
        GetRandomBook.DoGetRandomBook();
        TextboxEnable.EnableTextBox(true);
        LightTimer.EnableLights();
        LightTimer.EnableLightsAgain();
        LightTimer.DisableLights();
        LightTimer.DisableLightAgain();
        LightGroupChanger.EnableLightGroup();
        LightGroupChanger.DisableLightGroup();
        TurnOnWhiteCanvas.TurnOnWhiteCanvas();
        Debug.Log("The Spinning is complete");
        //Handle Restart Scene Here
        //Circle Pie Timer or a Bar changing in scale that when ends Restarts Scene
        // TextboxEnable.TextBox.SetActive(true);

    }

    // IEnumerator PressForNewBook(int delay)
    // {
    //     
    // }
    
}

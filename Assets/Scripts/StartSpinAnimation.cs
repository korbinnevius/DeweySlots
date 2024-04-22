using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class StartSpinAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool spinning;

    public GetRandomBook GetRandomBook;
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
        if (Input.GetKeyDown(KeyCode.Space) && !spinning )
        {
            animator.SetTrigger("Spinning");
            spinning = true;
        }
    }

    public void SpinningStopped()
    {
        spinning = false;
        GetRandomBook.DoGetRandomBook();
    }
}
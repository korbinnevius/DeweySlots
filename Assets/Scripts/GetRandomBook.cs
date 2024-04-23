using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class GetRandomBook : MonoBehaviour
    {
        public Catalog catalog;
        public TextMeshPro callNumText;
        public TextMeshPro titleText;
        public TextMeshPro authorText;
        public float resetTime;
        private Coroutine _inputResetCoroutine;

        private void Start()
        {
            _inputResetCoroutine = StartCoroutine(ResetTextAfterDelay());
        }

        private void Update()
        {
            if (Input.anyKeyDown)
            {
                if (_inputResetCoroutine != null)
                {
                    StopCoroutine(ResetTextAfterDelay());
                }

                _inputResetCoroutine = StartCoroutine(ResetTextAfterDelay());
            }
            
        }

        public void UpdateText()
        {
            var book = catalog.GetRandomAvailableBook();

            callNumText.text = book.callNumber;
            titleText.text = book.title;
            authorText.text = book.author;
            //text.asd = book.asdf
        }

        public void ClearSlotsText()
        {
            callNumText.text = " ";
            titleText.text = " ";
            authorText.text = " ";
        }

        public void DoGetRandomBook()
        {
            UpdateText();
        }

        IEnumerator ResetTextAfterDelay()
        {
            Debug.Log("Hey CoRoutine Started");
            yield return new WaitForSeconds(resetTime);
            titleText.text = "Press Button For Random Book";
            callNumText.text = "";
            authorText.text = " ";
        }
    }
}
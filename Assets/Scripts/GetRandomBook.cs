using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class GetRandomBook : MonoBehaviour
    {
        public Catalog catalog;
        public TextMeshPro callNumText;
        public TextMeshPro titleText;
        public TextMeshPro authorText;
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
    }
}
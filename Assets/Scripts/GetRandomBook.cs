using UnityEngine;

namespace DefaultNamespace
{
    public class GetRandomBook : MonoBehaviour
    {
        public Catalog catalog;

        public void UpdateText()
        {
            var book = catalog.GetRandomAvailableBook();
            //text.asd = book.asdf
            
        }
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    
    public class CatalogUpdater : MonoBehaviour
    {
        public string websiteURL = "https://libstaff.chatham.edu/api/v1/public/biblios/";
        public Catalog catalog;
        [FormerlySerializedAs("active")] public bool updateOnStart = true;
        void Start()
        {
            if (updateOnStart)
            {
                StartCoroutine(UpdateAvailableBooks());
            }
        }
        public IEnumerator UpdateAvailableBooks()
        {
            if (catalog.Count == 0)
            {
                yield break;
            }
            while (gameObject.activeInHierarchy)
            {
                //get next book that needs to be updated.
                int n = catalog.lastUpdatedIndex;
                n++;
                if (n > catalog.Count)
                {
                    n = 0;
                }

                yield return StartCoroutine(UpdateABook(catalog.GetBook(n)));
                catalog.lastUpdatedIndex = n;
                yield return new WaitForSeconds(0.5f);
            }
        }

        IEnumerator UpdateABook(Book book)
        {
            if (book == null)
            {
                yield break;
            }
            //"https://libstaff.chatham.edu/api/v1/public/biblios/10000/items";
            using (UnityWebRequest webRequest = UnityWebRequest.Get(websiteURL + book.biblio + "/items"))
            {
                //webRequest.SetRequestHeader("Accept", "application/marc");
                yield return webRequest.SendWebRequest();
                var o = "{\"items\":" + webRequest.downloadHandler.text + "}";
                CatalogPopulator.BiblioResult br = JsonUtility.FromJson<CatalogPopulator.BiblioResult>(o);
                if (br != null)
                {
                    bool isAvailable = false;
                    int floor = -2;
                    if (br.items != null)
                    {
                        foreach (Item item in br.items)
                        {
                            if (item.damaged_status == 1 || item.effective_not_for_loan_status == 1 || item.not_for_loan_status == 1)
                            {
                                //NOT available
                              //  continue;
                            }
                            if (string.IsNullOrEmpty(item.checked_out_date))
                            {
                                isAvailable = true;
                                floor = GetFloor(item.location);
                            }
                            else
                            {   
                              //  Debug.Log($"Unavailable: {item.checked_out_date}");
                            }
                        }
                    }
                    else
                    {
                        Debug.Log($"No items in biblio result. Error? {book.biblio}");
                    }

                    catalog.UpdateBook(book.biblio, floor, isAvailable);
                }
                else
                {
                    Debug.Log($"No Book: {book.biblio}");
                }
            }
        }

        private int GetFloor(string itemLocation)
        {
            //"CIRC3" = '3' = 3
            char floor = itemLocation[^1];
            if (int.TryParse(floor.ToString(), out int v))
            {
                return v;
            }

            return -1;
        }
    }
}
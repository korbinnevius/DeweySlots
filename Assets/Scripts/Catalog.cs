using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Catalog", menuName = "Catalog", order = 0)]
    public class Catalog : ScriptableObject
    {
        public int LastBiblioSearchedFor;
        public int lastUpdatedIndex;
        public int Count => Books.Count;
       //public List<Item> Items;
       public List<Book> Books;

        [SerializeField] private List<Book> availableBooks;
        // public void AddItem(Item item)
        // {
        //     Items.Add(item);
        // }
        public void AddBook(Book book)
        {
            if (Books.All(b => b.biblio != book.biblio))
            {
                Books.Add(book);
                Debug.Log("Book Added: "+book.title);
            }
        }

        public Book GetBook(int i)
        {
            if(i >= 0 && i< Books.Count)
            {
                return Books[i];
            }

            return null;
        }

        public void UpdateBook(int bookBiblio, int floor, bool isAvailable)
        {
            int index = Books.FindIndex(x => x.biblio == bookBiblio);
            if (index != -1)
            {
                Books[index].floor = floor;
                var c = Books[index].isAvailable;
                if (c != isAvailable)
                {
                    Books[index].isAvailable = isAvailable;
                    RefreshAvailable();
                }
            }
        }

        [ContextMenu("Refresh available")]
        private void RefreshAvailable()
        {
            //update our available cache.
            availableBooks = Books.Where(x => x.isAvailable).ToList();
        }
        public Book GetRandomAvailableBook()
        {
            if (availableBooks == null || availableBooks.Count == 0)
            {
                RefreshAvailable();
            }
            
            return availableBooks[UnityEngine.Random.Range(0, availableBooks.Count)];
        }
    }
}
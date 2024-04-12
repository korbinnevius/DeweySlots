using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Catalog", menuName = "Catalog", order = 0)]
    public class Catalog : ScriptableObject
    {
       public List<Item> Items;
       public List<Book> Books;
        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public void AddBook(Book book)
        {
            //todo: check for duplicates!
            Books.Add(book);
        }
    }
}
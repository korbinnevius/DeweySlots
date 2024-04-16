using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System;
using DefaultNamespace;
using MARC;

public partial class CatalogPopulator : MonoBehaviour
{
    public int maxBiblio = 100000;
    public Catalog catalog;
    public TextMeshProUGUI text;

    public string biblioFrontURL = "https://libstaff.chatham.edu/api/v1/public/biblios/";
    //Generating code to go to and from strings 
    [System.Serializable]
    public class BiblioResult : object
    {
        public Item[] items;
    }


    void Start()
    {
    }

    [ContextMenu("Get ALL the Books")]
    void GetAllBooks()
    {
        //clear the existing catalog, then start adding books.
        StartCoroutine(DoGetAllBooks());
    }

    IEnumerator DoGetAllBooks()
    {
        int delay = 0;
        int startOffset = catalog.LastBiblioSearchedFor;
        for (int i = startOffset; i < 100000; i++)
        {
            yield return StartCoroutine(AddBookIfExists(i));
            delay++;
            if (delay > 50)
            {
                yield return new WaitForSeconds(0.25f);
                delay = 0;
            }

        }
    }
    public void onRefresh()
    {
        Start();
    }

    IEnumerator AddBookIfExists(int biblio)
    {
        //string data = "{\"title\":\"media\"}";
        catalog.LastBiblioSearchedFor = biblio;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(biblioFrontURL+biblio.ToString()))
        {
           webRequest.SetRequestHeader("Accept","application/marc");
            yield return webRequest.SendWebRequest();
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                break;
                case UnityWebRequest.Result.Success:
                    FileMARC records = new FileMARC(webRequest.downloadHandler.text);
                    var record = records[0];
                    Book b = new Book();
                    //is available
                    b.author = GetMarcSubField(record, "100");
                    b.title = GetMarcSubField(record, "245");
                    b.callNumber = GetMarcSubField(record, "092");
                    b.biblio = biblio;
                    catalog.AddBook(b);
                    break;
            }
        }
    }

    

    
    public string GetMarcSubField(Record record, string fieldID, char subFieldID = 'a')
    {
        var field = record[fieldID];
        if (field is DataField dataField)
        {
            if (dataField[subFieldID] != null)
            {
                string data = dataField[subFieldID].Data;
                //checking to see if theres b or c values and adding them if theyre real
                // if (string.IsNullOrEmpty(dataField['b'].Data!=) )
                // {
                //     
                // }
                // else
                // {
                //     
                // }
                // string data2 = dataField['b'].Data;
                // string data3 = dataField['c'].Data;
                //not getting parts just values
                Debug.Log($"This is how many parts there are in this book {dataField[subFieldID].Data}");
                return data;
            }
        }

        return null;
    }
   
}

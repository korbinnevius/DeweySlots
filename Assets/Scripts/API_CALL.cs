using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System;
using DefaultNamespace;
using Unity.VisualScripting;
using Object = System.Object;

public partial class API_CALL : MonoBehaviour
{
    public Catalog catalog;
    public TextMeshProUGUI text;
    
    //Generating code to go to and from strings 
    [System.Serializable]
    public class BiblioResult : object
    {
        public Item[] items;
    }


    void Start()
    {
        StartCoroutine(GetRequest("https://libstaff.chatham.edu/api/v1/public/biblios/87596"));
    }

    public void onRefresh()
    {
        Start();
    }

    IEnumerator GetRequest(string uri)
    {
        //string data = "{\"title\":\"media\"}";
        
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
           webRequest.SetRequestHeader("Accept","application/marc-in-json");
            yield return webRequest.SendWebRequest();
            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    //log error if request error
                Debug.LogError(String.Format("Something Went Wrong: {0}", webRequest.error));
                break;
                case UnityWebRequest.Result.Success:
                    var o = "{\"items\":"+webRequest.downloadHandler.text+"}";
                    BiblioResult br = JsonUtility.FromJson<BiblioResult>(o);
                    if (br != null)
                    {
                        foreach (Item item in br.items)
                        {
                            if (item.withdrawn == 0)//todo: && not damaged && not lsot && not... etc
                            {
                                if (!string.IsNullOrEmpty(item.callnumber))
                                {
                                    catalog.AddItem(item);
                                }
                            }
                        }
                    }
                    break;
                    
            }
        }
    }
   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RestClient : MonoBehaviour
{
    private static RestClient _instancia;

    public static RestClient Instance
    {
        get
        {
            if (_instancia == null)
            {
                _instancia = FindObjectOfType<RestClient>();
                if (_instancia == null)
                {
                    GameObject go = new GameObject();
                    go.name = typeof(RestClient).Name;
                    _instancia = go.AddComponent<RestClient>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instancia;
        }
    }

    //GET
    public IEnumerator Get(string url, System.Action<PlayerList> callBack)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    PlayerList playerList = JsonUtility.FromJson<PlayerList>(jsonResult);
                    callBack(playerList);
                }
            }
        }
    }

    //DELETE
    public IEnumerator Delete(string url, int id)
    {
        string urlWithParams = string.Format("{0}{1}", url, id);
        using (UnityWebRequest www = UnityWebRequest.Delete(urlWithParams))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    Debug.Log(string.Format("Deleted Player with ID: {0}", id));
                }
            }
        }
    }

    //POST
    public IEnumerator Post(string url, Player newPlayer, System.Action<PlayerList> callBack)
    {
        string jsonData = JsonUtility.ToJson(newPlayer);
        using (UnityWebRequest www = UnityWebRequest.Post(url, jsonData))
        {
            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler.contentType = "application/json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));

            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    PlayerList playerList = JsonUtility.FromJson<PlayerList>(jsonResult);
                    callBack(playerList);
                }
            }
        }
    }
}

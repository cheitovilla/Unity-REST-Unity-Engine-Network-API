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
}

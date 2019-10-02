using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public string WEB_URL = "";

    // Start is called before the first frame update
    void Start()
    {
        //  StartCoroutine(RestClient.Instance.Get(WEB_URL, GetPlayers));

        StartCoroutine(RestClient.Instance.Post(WEB_URL, new Player
        {
            id = 5,
            fullname = "Thomas Shelby",
            score = 69
        }, GetPlayers));
    }

    void GetPlayers( PlayerList playerList)
    {
        foreach (Player player in playerList.players)
        {
            Debug.Log("Player ID: " + player.id);
            Debug.Log("Player Full Name: " + player.fullname);
            Debug.Log("Player Score: " + player.score);
        }
    }
}

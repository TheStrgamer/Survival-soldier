using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

/**
 * responsible for managing all players in the game
 * the player manager is server side
 * */

public class PlayerManager : NetworkBehaviour
{
    [SyncVar]
    private List<GameObject> players = new List<GameObject>();

    private int playerMoney = 0;

    [Server]
    public void AddPlayer(GameObject player)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] == null)
            {
                players[i] = player;
                Debug.Log("Player " + (players.IndexOf(player) + 1) + " joined");
            }

        }
        players.Add(player);
        Debug.Log("Player " + (players.IndexOf(player)+1) + " joined");
        RpcSetPlayerNumber(player, players);
    }
    //TODO: remove framecap
    void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 30;
    }

    [ClientRpc]
    public void RpcSetPlayerNumber(GameObject player, List<GameObject> players)
    {
        foreach (GameObject p in players)
        {
            p.GetComponent<PlayerScript>().setPlayerNumber(players.IndexOf(p) + 1);
        }
    }

    [Server]

    public void addMoney(int value)
    {
        playerMoney += value;
        RpcUpdateMoney(playerMoney);
    }

    [ClientRpc]
    public void RpcUpdateMoney(int value)
    {
        playerMoney = value;
        GameObject.Find("MoneyText").GetComponent<TMP_Text>().text = "$ " +  value.ToString(); ;
    }








}

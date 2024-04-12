using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManager : NetworkBehaviour
{
    [SyncVar]
    private List<GameObject> players = new List<GameObject>();

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

    [ClientRpc]
    public void RpcSetPlayerNumber(GameObject player, List<GameObject> players)
    {
        foreach (GameObject p in players)
        {
            p.GetComponent<PlayerScript>().setPlayerNumber(players.IndexOf(p) + 1);
        }
    }



    




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeBaseScript : MonoBehaviour
{
    public GameObject roof;
    private GameObject Player;
    private GameObject PlayerCamera;

    public void setPlayer(GameObject player)
    {
        Player = player;
        PlayerCamera = GameObject.Find("Main Camera").gameObject;
    }

    private void Update()
    {
        if (Player == null || PlayerCamera == null) {return;}
        RaycastHit hit;
        Vector3 dir = Player.transform.position - PlayerCamera.transform.position;
        if (Physics.Raycast(PlayerCamera.transform.position, dir, out hit, 1000))
        {
            if (hit.collider.gameObject == roof)
            {
                roof.GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                roof.GetComponent<MeshRenderer>().enabled = true;
            }
        }


    }

}

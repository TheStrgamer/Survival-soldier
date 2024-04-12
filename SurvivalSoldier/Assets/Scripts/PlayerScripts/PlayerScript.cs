using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : NetworkBehaviour
{
    [SyncVar]
    [SerializeField] private int playerNumber = -1;

    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth = 100f;

    [SerializeField] private Camera playerCamera;

    //Components
    private playerColorPicker[] playerColorPickers;
    private PlayerMovement playerMovement;
    private PlayerManager playerManager;






    [Client]
    void Start()
    {
        
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        playerMovement = GetComponent<PlayerMovement>();
        playerColorPickers = GetComponentsInChildren<playerColorPicker>();
        playerManager.AddPlayer(gameObject);


        if (!isOwned) {
            return; 
        }
        playerCamera = Camera.main;
    }

    void Update()
    {
        if (!isOwned) { return; }
        playerCamera.transform.position = new Vector3(transform.position.x, playerCamera.transform.position.y, transform.position.z-9.22f);
        
    }

    [Client]
    public void setPlayerNumber(int playerNumber)
    {
        this.playerNumber = playerNumber;
        playerColorPickers = GetComponentsInChildren<playerColorPicker>();
        foreach (playerColorPicker playerColorPicker in playerColorPickers)
        {
            playerColorPicker.SetMaterial(playerNumber-1);
        }
    }

}

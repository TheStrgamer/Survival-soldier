using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : NetworkBehaviour
{
    [SyncVar]
    [SerializeField] private int playerNumber = -1;

    
    [SerializeField] private Camera playerCamera;

    //Components
    private playerColorPicker[] playerColorPickers;
    private PlayerMovement playerMovement;
    private PlayerManager playerManager;
    private PlayerStatsManager playerStatsManager;

    public bool canMove = true;
    public bool canMine = true;
    public bool canOpenInventory = true;

    private PlayerHarvesting playerHarvesting;
    private PlayerInventory playerInventory;


    [Client]
    void Start()
    {
       
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        playerMovement = GetComponent<PlayerMovement>();
        playerColorPickers = GetComponentsInChildren<playerColorPicker>();
        playerHarvesting = GetComponent<PlayerHarvesting>();
        playerInventory = GetComponent<PlayerInventory>();
        playerStatsManager = GetComponent<PlayerStatsManager>();

        playerManager.AddPlayer(gameObject);


        if (!isOwned) {
            return; 
        }
        playerCamera = Camera.main;
        GameObject.FindWithTag("HomeBase").GetComponent<HomeBaseScript>().setPlayer(gameObject);
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

    [Client]
    public void setCanMove(bool canMove)
    {
        this.canMove = canMove;
        playerMovement.setCanMove(canMove);
    }

    [Client]
    public void setCanMine(bool canMine)
    {
        this.canMine = canMine;
        playerHarvesting.setCanMine(canMine);
    }
    [Client]
    public void setCanOpenInventory(bool canOpenInventory)
    {
        this.canOpenInventory = canOpenInventory;
        playerInventory.setCanOpenInventory(canOpenInventory);
    }

    [Client]
    public void addMoney(int amount)
    {
        playerStatsManager.addMoney(amount);
    }

}

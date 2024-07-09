using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class playerStateManager : NetworkBehaviour
{
    

    public bool isHarvesting = false;

    public bool isAiming = false;
    public bool isShooting = false;

    public bool isWalking = false;
    public bool isRunning = false;

    public bool harvestMode = true;
    public bool combatMode = false;

    private ImageSwitcher iconSwithcer;


    public KeyCode aimModeButton = KeyCode.LeftControl;


    PlayerMovement playerMovement;
    PlayerHarvesting playerHarvesting;
    PlayerScript playerScript;
    PlayerShootingManager playerShootingManager;
    PlayerDragging playerDragging;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerHarvesting = GetComponent<PlayerHarvesting>();
        playerScript = GetComponent<PlayerScript>();
        playerShootingManager = GetComponent<PlayerShootingManager>();
        iconSwithcer = GameObject.Find("ModeIndicator").GetComponent<ImageSwitcher>();
        playerDragging = GetComponent<PlayerDragging>();
        if (iconSwithcer != null && isOwned)
        {
            iconSwithcer.SwitchImage(0);
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(aimModeButton))
        {
            setIsAiming(!isAiming);
        }
        
    }

    void setIsAiming(bool value)
    {
        if (!isOwned) { return; }

        harvestMode = !value;
        combatMode = value;
        playerDragging.setCanDrag(harvestMode);
        if (iconSwithcer != null)
        {
            iconSwithcer.SwitchImage(value ? 1 : 0);
        }

        isAiming = value;
        //playerMovement.setRotationToMouse(value);
        playerShootingManager.setCanShoot(value);

        playerHarvesting.setCanMine(!value);

        if (value)
        {
            isHarvesting = false;
            isRunning = false;
            playerScript.setDistanceMultiplier(.25f);
        }
        else
        {
            playerScript.setDistanceMultiplier(0f);
        }
    }

    void setIsShooting(bool value)
    {
        if (!isOwned) { return; }
        isShooting = value;
        isAiming = value;
        if (value)
        {
            isHarvesting = false;
            isRunning = false;
        }
    }

    void setIsHarvesting(bool value)
    {
        if (!isOwned) { return; }
        isHarvesting = value;
        if (value)
        {
            isAiming = false;
            isRunning = false;
        }
    }

    void setIsRunning(bool value)
    {
        if (!isOwned) { return; }
        isRunning = value;
        if (value)
        {
            isAiming = false;
            isHarvesting = false;
        }
    }

}

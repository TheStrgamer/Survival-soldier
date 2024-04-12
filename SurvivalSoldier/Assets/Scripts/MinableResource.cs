using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinableResource : NetworkBehaviour
{
    [SerializeField] private GameObject[] resourceModels;
    [SerializeField] private string resourceName;

    [SerializeField] private int maxHealth = 10;
    [SyncVar] private int currentHealth;

    [SerializeField] public Item droppedItem;

    [SyncVar] private int randomModel = -1;

    private bool mineAnimation = false;
    private float mineAnimationTime = 0.1f;
    private float mineAnimationMinScale = 0.9f;
    private Vector3 goalScale = Vector3.one;

    
    void Start()
    {
        currentHealth = maxHealth;
        if (randomModel == -1)
        {
            return;
        }
        for (int i = 0; i < resourceModels.Length; i++)
        {
            if (i == randomModel)
            {
                resourceModels[i].SetActive(true);
            }
            else
            {
                Destroy(resourceModels[i]);
            }
        }
    }

    public int getModelsLength()
    {
        return resourceModels.Length;
    }

    public void setModel(int model)
    {
        randomModel = model;
    }

    private void Update()
    {
        if (mineAnimation)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, goalScale, mineAnimationTime);
            if (transform.localScale.x >= goalScale.x*0.99f)
            {
                mineAnimation = false;
                transform.localScale = goalScale;
            }
        }
    }
    /**
     * Deals damage to the resource and destroys it if it's health is 0 or less
     */
    [ClientRpc]
    public void MineResource(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            DropItem();
            NetworkServer.Destroy(gameObject);
        }
    }

    /**
    * Spawns the dropped item
    */
    public void DropItem()
    {
        if (droppedItem == null) { return; }
    }

    public int getHealth()
    {
        return currentHealth;
    }


    [ClientRpc]
    public void startMineAnim()
    {
        mineAnimation = true;
        goalScale = transform.localScale;
        transform.localScale = goalScale * mineAnimationMinScale;

    }

}

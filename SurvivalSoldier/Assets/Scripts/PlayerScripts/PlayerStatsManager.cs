using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;

public class PlayerStatsManager : NetworkBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth = 100;


    [Client]

    public void TakeDamage(int damage)
    {
        if (!isOwned) { return; }

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    [Client]

    public void Die()
    {
        Debug.Log("Player died");
    }

}

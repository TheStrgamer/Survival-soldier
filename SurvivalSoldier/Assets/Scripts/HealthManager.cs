using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    float maxHealth = 100;
    float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            die();
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    public void addHealth(float health)
    {
        currentHealth += health;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void die() {         
        Destroy(gameObject);
       }

}

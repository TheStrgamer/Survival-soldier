using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : NetworkBehaviour
{
    [SerializeField] private float maxHealth = 100;
    private float currentHealth;

    public bool destroyOnDeath = true;
    public bool invincible = false;

    public bool playHitAnim = true;

    private bool hurtAnim = false;
    private Vector3 goalScale = Vector3.one;
    private float hurtAnimationTime = 0.05f;
    private float hurtAnimationMinScale = 0.9f;


    void Start()
    {
        currentHealth = maxHealth;
        
    }

    void Update()
    {

        if (hurtAnim)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, goalScale, hurtAnimationTime);
            if (transform.localScale.x >= goalScale.x * 0.99f)
            {
                hurtAnim = false;
                transform.localScale = goalScale;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (playHitAnim)
        {
            startHurtAnim();

        }
        if (currentHealth <= 0)
        {
            die();
        }
    }

    [ClientRpc]
    public void addHealth(float health)
    {
        currentHealth += health;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void die() {   
        if (destroyOnDeath)
        {
            NetworkServer.Destroy(gameObject);
        }
    }

    [ClientRpc]
    public void startHurtAnim()
    {
        hurtAnim = true;
        goalScale = transform.localScale;
        transform.localScale = goalScale * hurtAnimationMinScale;

    }

}

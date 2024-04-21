using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatsManager : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth = 100;

    [SerializeField] private int money = 0;
    private TMP_Text moneyText;

    private void Start()
    {
        moneyText = GameObject.Find("MoneyText").GetComponent<TMP_Text>();
        moneyText.text ="$ " + money.ToString() ;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Debug.Log("Player died");
    }

    public void addMoney(int amount)
    {
        money += amount;
        moneyText.text ="$ " + money.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile", menuName = "Inventory/Projectile")]
public class projectile : ScriptableObject
{
    
    public new string name;

    public float damage;

    public float projectileSpeed;
    public float projectileLifetime;

    public GameObject impactEffect;

}

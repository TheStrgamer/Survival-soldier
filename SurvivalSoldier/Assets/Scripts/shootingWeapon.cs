using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]

public class shootingWeapon : ScriptableObject
{

    public Sprite icon;
    public new string name;
    public int value;
    public float damageMultiplier;

    public float fireRate;

    public int maxAmmo;
    public int currentAmmo;

    public GameObject projectilePrefab;
    public GameObject gunModel;

}

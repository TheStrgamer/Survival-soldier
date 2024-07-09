using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerShootingManager : NetworkBehaviour
{

    public shootingWeapon currentWeapon;
    public shootingWeapon[] weapons;

    public float timeSinceLastShot = 0;

    public int currentWeaponIndex = 0;

    private KeyCode fireWeaponKey = KeyCode.Mouse0;
    public string actionName = "Action1";



    [SyncVar] public bool canShoot;

    public Transform gunPoint;

    public GameObject gunModel;

    private PlayerScript playerScript;

    public void Start()
    {
        currentWeapon = weapons[currentWeaponIndex];
        canShoot = false;

        if (!isOwned) { return; }
        playerScript = GetComponent<PlayerScript>();
        fireWeaponKey = InputManager.GetKeyCode(actionName);
    }

    [Client]
    void Update()
    {

        gunModel.SetActive(canShoot);
        if (!isOwned) { return; }

        if (canShoot)
        {
            if (Input.GetKey(fireWeaponKey) && timeSinceLastShot >= currentWeapon.fireRate)
            {
                timeSinceLastShot = 0;

                CmdShoot();
                playerScript.cameraShake(0.05f, 0.05f);

            }

            timeSinceLastShot += Time.deltaTime;


        }

    }

    [Client]
    public void setCanShoot(bool value)
    {
        canShoot = value;
    }

    [Command]
    public void CmdShoot()
    {
        Quaternion rotationQuaternion = Quaternion.Euler(
            currentWeapon.projectilePrefab.transform.eulerAngles.x,
            this.transform.eulerAngles.y,
            currentWeapon.projectilePrefab.transform.eulerAngles.z
        );


        GameObject bullet = Instantiate(currentWeapon.projectilePrefab, gunPoint.position, rotationQuaternion);
        bullet.GetComponent<ProjectileScript>().SetDirection(gunPoint.forward);
        bullet.GetComponent<ProjectileScript>().addTargetTag("Enemy");
        NetworkServer.Spawn(bullet);
    }
}

using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHarvesting : NetworkBehaviour
{
    [SerializeField] private float harvestRange = 1f;
    [SerializeField] private int harvestPower = 1;
    private int extraHarvestPower = 0;

    [SerializeField] private float harvestCooldown = 1f;
    [SerializeField] private float currentHarvestCooldown = 0f;

    [SerializeField] private LayerMask layerMask;
//    [SerializeField] private Inventory inventory;

    [SerializeField] private KeyCode harvestKey = KeyCode.E;

    [Client]
    private void Update()
    {
        if (!isOwned) return;
        currentHarvestCooldown -= Time.deltaTime;
        if (Input.GetKeyDown(harvestKey) && currentHarvestCooldown <=0)
        {
            Debug.Log("Harvesting");
            Harvest();
            currentHarvestCooldown = harvestCooldown;
        }
        Debug.DrawLine(transform.position, transform.position + transform.forward * harvestRange, Color.red, Time.deltaTime);

    }
    [Client]
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * 0.7f, harvestRange);
    }

    [Client]
    private void Harvest()
    {
        if (!isOwned) return;
        Vector3 point = transform.position + transform.forward * 0.7f;
        Collider[] colliders = Physics.OverlapSphere(point, harvestRange, layerMask);
        if (colliders.Length > 0)
        {
            Collider closestCollider = colliders[0];
            float closestDistance = Mathf.Infinity;

            foreach (Collider collider in colliders)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestCollider = collider;
                }
            }
            MinableResource resource = closestCollider.GetComponentInParent<MinableResource>();
            if (resource != null)
            {
                CmdMineResource(resource);
            }
        }
    }

    [Client]
    public void SetExtraPower(int power)
    {
        if (!isOwned) return;
        extraHarvestPower = power;
    }

    [Command]
    public void CmdMineResource(MinableResource resource)
    {
        resource.MineResource(harvestPower + extraHarvestPower);
        resource.startMineAnim();
    }
}

using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class EnemyScript : NetworkBehaviour
{

    [SyncVar] public GameObject currentTarget;
    public  GameObject mainBase;

    [SerializeField] private float targetChangeTime = 1f;
    private float currentTargetChangeTime = 0f;
    [SerializeField] private float targetDistance = 5f;

    [SerializeField] private LayerMask targetLayerMask;

    [SerializeField] private bool prioritizeMainBase = true;

    private EnemyMovementScript movementScript;

    [Server]
    void Start()
    {
        mainBase = GameObject.Find("HomeBaseDefenceWall");
        movementScript = GetComponent<EnemyMovementScript>();
        
    }

    [Server]
    void Update()
    {

        if (currentTarget == null)
        {
            refreshTarget();
        }
        else if (currentTargetChangeTime <= 0)
        {
            refreshTarget();
        }
        currentTargetChangeTime -= Time.deltaTime;


    }
    [Server]
    private void refreshTarget()
    {
        currentTargetChangeTime = targetChangeTime;
        GameObject[] targets = getGameObjectsInRange(targetDistance);
        if (targets.Length == 0) { 
            setTarget(mainBase);
            return;
        }
        if (currentTarget != null && targets.Contains(currentTarget))
        {
            if (prioritizeMainBase && Vector3.Distance(currentTarget.transform.position, mainBase.transform.position) < targetDistance*2.5f)
            {
                setTarget(mainBase);
            }
            return;

        }
        else
        {
            if (prioritizeMainBase)
            {
                setTarget(mainBase);
            }
            else
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    if (targets[i].GetComponent<NetworkIdentity>() != null)
                    {
                        setTarget(targets[i]);
                        return;
                    }
                    else if (targets[i].GetComponentInParent<NetworkIdentity>() != null)
                    {
                        setTarget(targets[i].GetComponentInParent<NetworkIdentity>().gameObject);
                        return;
                    }
                }
                setTarget(mainBase);

            }
        } 
        
            
    }

    [Server]
    private Collider[] getTargetsInRange(float range)
    {
        return Physics.OverlapSphere(transform.position, range, targetLayerMask);
    }

    [Server]
    private GameObject[] getGameObjectsInRange(float range)
    {
        Collider[] colliders = getTargetsInRange(range);
        GameObject[] gameObjects = new GameObject[colliders.Length];
        for (int i = 0; i < colliders.Length; i++)
        {
            gameObjects[i] = colliders[i].gameObject;
        }
        return gameObjects;

    }

    [ClientRpc] 
    public void setTarget(GameObject target)
    {
        currentTarget = target;
        if ( target != null && movementScript != null )
        {
            movementScript.setTarget(target.transform.position);

        }
    }
}

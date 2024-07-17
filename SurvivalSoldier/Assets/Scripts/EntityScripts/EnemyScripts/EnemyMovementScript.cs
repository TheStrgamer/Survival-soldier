using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovementScript : NetworkBehaviour
{

    private Vector3 currentTarget;

    private NavMeshAgent agent;

    [SerializeField] private float moveSpeed = 4f;
    private float moveSpeedMultiplier = 1f;

    [SerializeField] private float stoppingDistance = 1f;
    [SerializeField] private float stoppingDistanceOffsetRange = 0.5f;
    private float offset;
    private float currentStoppingDistance;

    private float navMeshUpdateRate = 0.1f;
    private float currentNavMeshUpdateRate = 0f;

    public bool isMoving = false;

    private float rotationSpeed = 720f;


    [Client]
    void Start()
    {
        offset = Random.Range(-stoppingDistanceOffsetRange, stoppingDistanceOffsetRange);
        currentStoppingDistance = stoppingDistance + offset;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.stoppingDistance = 0f;

        currentNavMeshUpdateRate = navMeshUpdateRate;
        
    }

    [Client]
    void Update()
    {
        currentStoppingDistance = stoppingDistance + offset;

        currentNavMeshUpdateRate -= Time.deltaTime;
        if (currentNavMeshUpdateRate <= 0)
        {
            agent.SetDestination(getTargetPosition());
            currentNavMeshUpdateRate = navMeshUpdateRate;
        }
        if (agent.remainingDistance <= 0.1f)
        {
            isMoving = false;
        } else
        {
            isMoving = true;
        }
        if (isMoving)
        {
            agent.isStopped = false;
        } else
        {
            agent.isStopped = true;
            SmoothLookAtTarget(currentTarget);

        }

        
        
    }

    [Client]
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(getTargetPosition(), 1f);
    }

    private Vector3 getTargetPosition()
    {
        return currentTarget - (currentTarget - transform.position).normalized * currentStoppingDistance;
    }

    [ClientRpc]
    public void setTarget(Vector3 target)
    {
        if (target == null) { return; }
        currentTarget = target;
    }

    private void SmoothLookAtTarget(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }


}

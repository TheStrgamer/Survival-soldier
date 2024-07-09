using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerDragging : NetworkBehaviour
{

    [SerializeField] private float dragRange = 1f;
    private Rigidbody draggedRigidBody;

    [SerializeField] private LayerMask layers;
    [SerializeField] private string actionName = "Action1";
    private KeyCode dragKey = KeyCode.E;

    private LineRenderer lineRenderer;

    [SyncVar] private Vector3 lineStartPos;
    [SyncVar] private Vector3 lineEndPos;

    private bool canDrag = true;




    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);

        dragKey = InputManager.GetKeyCode(actionName);


    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, lineStartPos);
        lineRenderer.SetPosition(1, lineEndPos);
        if (!isOwned || !canDrag) return;
        if (Input.GetKeyDown(dragKey))
        {
            setDragObject();
        }
        if (Input.GetKey(dragKey))
        {
            Drag();
        }
        if (Input.GetKeyUp(dragKey))
        {
            if (draggedRigidBody != null)
            {
                CmdSetBeingDragged(draggedRigidBody.gameObject, null);
                draggedRigidBody = null;
            }

            CmdUpdateLineRenderer(transform.position, transform.position);
        }



    }

    [Client]
    private void setDragObject()
    {
        if (!isOwned) return;

        Vector3 point = transform.position + transform.forward * dragRange;
        Collider[] colliders = Physics.OverlapSphere(point, dragRange, layers);
        
        if (colliders.Length > 0)
        {
            draggedRigidBody = colliders[0].GetComponent<Rigidbody>();
            if (draggedRigidBody != null)
            {
                if (draggedRigidBody.gameObject.GetComponent<DraggableObject>().getIsBeingDragged() == false)
                {
                    CmdSetBeingDragged(draggedRigidBody.gameObject, this.gameObject);

                }
                else
                {
                    draggedRigidBody = null;
                }
            }

        }
    }
    [Client]
    private void Drag()
    {
        if (!isOwned) return;
        if (draggedRigidBody == null)
        {
            CmdUpdateLineRenderer(transform.position, transform.position); return;
        }


        float distance = Vector3.Distance(transform.position, draggedRigidBody.transform.position);

        float maxSpeed = 6f;
        if (distance > dragRange && draggedRigidBody.velocity.magnitude < maxSpeed)
        {
            if (draggedRigidBody != null)
                CmdAddForceToDraggedObject(draggedRigidBody.gameObject, (transform.position - draggedRigidBody.transform.position).normalized * (distance - dragRange) * 450 * Time.deltaTime);

        }
        CmdUpdateLineRenderer(transform.position, draggedRigidBody.transform.position);
        

    }
    [Command]
    private void CmdSetBeingDragged(GameObject draggedObject, GameObject player)
    {
        if (draggedObject == null) return;
        DraggableObject draggable = draggedObject.GetComponent<DraggableObject>();
        draggable.setBeingDragged(player);
    }

    [Command]
    private void CmdUpdateLineRenderer(Vector3 startPosition, Vector3 endPosition)
    {
        lineStartPos = startPosition;
        lineEndPos = endPosition;

    }
    [Command]
    private void CmdAddForceToDraggedObject(GameObject draggedObject, Vector3 force)
    {
        if (draggedObject == null) {
            CmdUpdateLineRenderer(transform.position, transform.position);
            return; 
        }
        Rigidbody rb = draggedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(force);
        }
    }

    [Client]
    public void setCanDrag(bool value)
    {
        canDrag = value;
        if (!canDrag)
        {
            if (draggedRigidBody != null)
            {
                CmdSetBeingDragged(draggedRigidBody.gameObject, null);
                draggedRigidBody = null;
            }
        }
    }

}

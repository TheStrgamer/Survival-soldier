using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayerMovement : NetworkBehaviour
{

    private Vector3 movement;
    private Rigidbody rb = null;
    [SerializeField] private float movementSpeed = 5f;

    [SerializeField] private bool rotationToMouse = false;

    [SerializeField] private float rotationSpeed = 25f;
    [SerializeField] private float jumpForce = 5f;

    private bool grounded = false;
    [SerializeField] private float groundCheckDistance = 0.2f;
    [SerializeField] Transform groundCheck = null;

    private playerColorPicker[] playerColorPickers;

    private bool canMove = true;

    public void setCanMove(bool canMove)
    {
        this.canMove = canMove;
    }


    [Client]
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    [Client]
    private void Update()
    {
        if (!isOwned) { return; }
        if (!canMove) { return; }
        movement = getMovementInput();
        playerMove();
        playerRotate();

        grounded = touchGround(groundCheckDistance);

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

    }

    [Client]
    private Boolean touchGround(float maxDist)
    {
        RaycastHit hit;
        if (groundCheck == null) { return false; }
            Vector3 endPoint = groundCheck.position + Vector3.down * maxDist;

            Debug.DrawLine(groundCheck.position, endPoint, Color.red);
 
        if (Physics.OverlapSphere(groundCheck.position, maxDist).Length > 1)
        {
            return true;
        }
  
        return false;
    }

    [Client]
    private Vector3 getMovementInput()
    {
        int x = 0;
        int z = 0;
        if (Input.GetKey(KeyCode.W)){z += 1;}
        if (Input.GetKey(KeyCode.S)) { z -= 1;}
        if (Input.GetKey(KeyCode.A)) { x -= 1;}
        if (Input.GetKey(KeyCode.D)) { x += 1;}

        return new Vector3(x, 0, z).normalized;

    }



    [Client]
    private void playerMove()
    {
        rb.MovePosition(rb.position + movement * movementSpeed * Time.deltaTime);
    }

    [Client]
    private void playerRotate()
    {
        if (!rotationToMouse)
        {
            if (movement == Vector3.zero) { return; }
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                Vector3 lookAt = hit.point;
                lookAt.y = transform.position.y;
                transform.LookAt(lookAt);
            }
        }
    }

    [Client]
    public void setRotationToMouse(bool value)
    {
        rotationToMouse = value;
    }

}

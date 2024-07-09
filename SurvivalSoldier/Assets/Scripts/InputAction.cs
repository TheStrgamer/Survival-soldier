using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputAction
{
    public KeyCode key { get; private set; }
    public bool isPressed { get; private set; }
    public bool isPressedLong { get; private set; }
    public bool isHeld { get; private set; }
    public bool isInputted { get; private set; }

    private float timeBetweenPresses = 0.15f;
    private float timeBetweenPressesLong = 0.5f;
    private float timeSinceLastPress = 0;

    public InputAction(KeyCode key)
    {
        this.key = key;
    }

    public void Update()
    {

        isPressed = false;
        isPressedLong = false;
        isHeld = false;
        isInputted = false;

        if (Input.GetKey(key))
        {
            isInputted = true;
            timeSinceLastPress += Time.deltaTime;
        }

        if (Input.GetKeyDown(key))
        {
            timeSinceLastPress = 0;
        }

        if (timeSinceLastPress > timeBetweenPressesLong)
        {
            isHeld = true;
        }
        else if (Input.GetKeyUp(key))
        {
             if (timeSinceLastPress < timeBetweenPresses)
             {
                 isPressed = true;
             }
             else if (timeSinceLastPress < timeBetweenPressesLong)
             {
                 isPressedLong = true;
             }

        }
    }

    public void simpleUpdate() {         
        isPressed = Input.GetKeyDown(key);
        isHeld = Input.GetKey(key);
        isInputted = isHeld;
    }
}

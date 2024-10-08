using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class textPopUp : MonoBehaviour
{
    
    public TMP_Text text;
    public Vector3 moveDirection;

    public float scaleSpeed = 1f;
    Vector3 defaultMove = new Vector3(0, 1, 0);


    public void init(Vector3 moveDirection, Color color, string text = "", float scaleSpeed = -0.5f)
    {
        this.text.text = text;
        this.moveDirection = moveDirection;
        this.text.color = color;
        this.scaleSpeed = scaleSpeed;
    }


    // Update is called once per frame
    void Update()
    {       
        transform.position += moveDirection * Time.deltaTime;
        if (transform.localScale.x+ scaleSpeed * Time.deltaTime < 0) { return; }
        transform.localScale += new Vector3(scaleSpeed, scaleSpeed, scaleSpeed) * Time.deltaTime;
    }
}

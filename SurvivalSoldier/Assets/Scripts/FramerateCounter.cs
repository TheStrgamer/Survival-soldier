using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FramerateCounter : MonoBehaviour
{
    // Start is called before the first frame update
    float timeSinceLastUpdate = 0;
    int frameCount = 0;
    public TMP_Text text;

    void Start()
    {
        text = GetComponent<TMP_Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastUpdate += Time.deltaTime;
        frameCount++;
        if (timeSinceLastUpdate >= 1)
        {
            text.text = frameCount + " fps";
            timeSinceLastUpdate = 0;
            frameCount = 0;
        }
        
    }
}

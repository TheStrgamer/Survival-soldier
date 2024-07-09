using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSwitcher : MonoBehaviour
{

    public Image[] images;
    public int currentImageIndex = 0;
    void Start()
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].enabled = i == currentImageIndex;
        }
        
    }

    public void SwitchImage(int index)
    {
        if (index < 0 || index >= images.Length) return;
        images[currentImageIndex].enabled = false;
        images[index].enabled = true;
        currentImageIndex = index;
    }


}

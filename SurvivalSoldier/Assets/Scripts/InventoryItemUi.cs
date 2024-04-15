using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemUi : MonoBehaviour
{
    private Image background;
    private RawImage icon;
    private TMP_Text count;

    private void Start()
    {
        background = GetComponent<Image>();
        icon = GetComponentInChildren<RawImage>();
        count = GetComponentInChildren<TMP_Text>();

        background.enabled = false;
        icon.enabled = false;
        count.enabled = false;
    }

    public void SetItem(Sprite sprite, int ammount)
    {
        background.enabled = true;
        icon.enabled = true;
        count.enabled = true;

        icon.texture = sprite.texture;
        count.text = ammount.ToString();
        
    }

    public void ClearItem()
    {
        background.enabled = false;
        icon.enabled = false;
        count.enabled = false;
    }

    public void enableBackground() {         background.enabled = true;
       }
}

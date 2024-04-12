using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public float pickupRange = 3f;
    public LayerMask layerMask;

    public SpriteRenderer spriteRenderer;

    private void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pickupRange, layerMask);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                Debug.Log("Player is in range");
            }
        }
    }
    public void SetItem(Item item)
    {
        Sprite sprite = item.GetSprite();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null || sprite == null) {return;}

        spriteRenderer.sprite = sprite;
        spriteRenderer.material = new Material(Shader.Find("Sprites/Default"));
        spriteRenderer.material.color = Color.white; // Set color to white for full opacity
    }
}

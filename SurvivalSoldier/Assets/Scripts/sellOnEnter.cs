using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sellOnEnter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInParent<PlayerInventory>())
        {
            other.gameObject.GetComponentInParent<PlayerInventory>().sellAllItems();
        }
        
    }
}

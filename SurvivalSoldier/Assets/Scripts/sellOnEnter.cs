using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sellOnEnter : MonoBehaviour
{

    [SerializeField]private PlayerManager playerManager;

    private void Start()
    {
        if (playerManager == null)
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<DraggableObject>() != null && other.gameObject.tag == "Sellable")
        {
            int value = other.GetComponent<DraggableObject>().getSellValue();
            Debug.Log("Sold for: " + value);
            playerManager.addMoney(value);
            Destroy(other.gameObject);


        }
        //if (other.gameObject.GetComponentInParent<PlayerInventory>())
        //{
        //    other.gameObject.GetComponentInParent<PlayerInventory>().sellAllItems();
        //}
        
    }
}

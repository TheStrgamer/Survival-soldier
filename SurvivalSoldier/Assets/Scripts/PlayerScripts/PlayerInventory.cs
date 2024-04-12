using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : NetworkBehaviour
{
    [SerializeField]
    public List<InventoryItem> inventoryItems = new List<InventoryItem>();
    public List<string> inventoryInspectorDisplay = new List<string>();
    public int maxHeldItems = 5; // Initial size of the inventory

    private int getCurrentItemCount()
    {
        int count = 0;
        foreach (InventoryItem item in inventoryItems)
        {
            count += item.count;
        }
        return count;
    }

    public bool canAddItem()
    {
        if (getCurrentItemCount() < maxHeldItems)
        {
            return true;
        }
        return false;
    }

    public void addItem(Item item, int count = 1)
    {
        if (canAddItem() && item != null)
        {
            InventoryItem inventoryItem = inventoryItems.Find(x => x.item == item);
            if (inventoryItem != null)
            {
                inventoryItem.count += count;
            }
            else
            {
                InventoryItem newItem = new InventoryItem();
                newItem.item = item;
                newItem.count = count;
                inventoryItems.Add(newItem);
            }
 
        }
    }
    public void removeItem(Item item, int count=0)
    {
        InventoryItem inventoryItem = inventoryItems.Find(x => x.item == item);
        if (inventoryItem != null)
        {
            if (count == 0 || inventoryItem.count <= count)
            {
                inventoryItems.Remove(inventoryItem);
            }
            else
            {
                inventoryItem.count -= count;
            }
        }
    }

    private void Update()
    {
        inventoryInspectorDisplay.Clear();
        foreach (InventoryItem item in inventoryItems)
        {
            inventoryInspectorDisplay.Add(item.item.name + " x" + item.count);
        }
    }

}

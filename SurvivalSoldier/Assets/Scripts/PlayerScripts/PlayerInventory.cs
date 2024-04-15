using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : NetworkBehaviour
{
    [SerializeField]
    public List<InventoryItem> inventoryItems = new List<InventoryItem>();
    public List<string> inventoryInspectorDisplay = new List<string>();
    public int maxHeldItems = 5; // Initial size of the inventory

    public GameObject inventoryDisplay;
    private InventoryItemUi[] inventoryItemUi;
    private TMP_Text inventoryCount;
    private GameObject inventoryFull;

    public KeyCode inventoryKey = KeyCode.I;

    private PlayerScript playerScript;
    private bool canOpenInventory = true;



    [Client]
    private void Start()
    {
        inventoryFull = GameObject.Find("Full_Inventory");
        inventoryDisplay = GameObject.Find("Inventory_back");
        inventoryCount = GameObject.Find("Inventory_count").GetComponent<TMP_Text>();

        inventoryItemUi = inventoryDisplay.GetComponentsInChildren<InventoryItemUi>();
        inventoryDisplay.SetActive(false);
        inventoryItems.Clear();
        playerScript = GetComponent<PlayerScript>();



    }

    public void setCanOpenInventory(bool canOpenInventory)
    {
        this.canOpenInventory = canOpenInventory;
    }

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

    [Client]
    private void Update()
    {
        inventoryInspectorDisplay.Clear();
        foreach (InventoryItem item in inventoryItems)
        {
            inventoryInspectorDisplay.Add(item.item.name + " x" + item.count);
        }
        if (Input.GetKeyDown(inventoryKey))
        {
            ToggleInventory();
            UpdateInventoryDisplay();
        }
        if (inventoryFull != null)
        {
            if (getCurrentItemCount() >= maxHeldItems)
            {
                inventoryFull.SetActive(true);
            }
            else
            {
                inventoryFull.SetActive(false);
            }
        }
        inventoryCount.SetText(getCurrentItemCount() + "/" + maxHeldItems + " items carried");

    }

    public void ToggleInventory()
    {
        if (!canOpenInventory) return;

        inventoryDisplay.SetActive(!inventoryDisplay.activeSelf);
        Debug.Log("Inventory Display: " + inventoryDisplay.activeSelf);
        playerScript.setCanMove(!inventoryDisplay.activeSelf);
        playerScript.setCanMine(!inventoryDisplay.activeSelf);
    }

    public void UpdateInventoryDisplay()
    {
        for (int i = 0; i < inventoryItemUi.Length; i++)
        {
            if (i < inventoryItems.Count)
            {
                inventoryItemUi[i].SetItem(inventoryItems[i].item.icon, inventoryItems[i].count);
            }
            else
            {
                inventoryItemUi[i].ClearItem();
            }
        }

    }


}

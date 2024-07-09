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

    private TMP_Text sellText;

    public GameObject textPopUp = null;



    [Client]
    private void Start()
    {
        if (!isOwned) { return; }
        inventoryFull = GameObject.Find("Full_Inventory");
        inventoryDisplay = GameObject.Find("Inventory_back");
        inventoryCount = GameObject.Find("Inventory_count").GetComponent<TMP_Text>();

        inventoryItemUi = inventoryDisplay.GetComponentsInChildren<InventoryItemUi>();
        inventoryDisplay.SetActive(false);
        inventoryItems.Clear();
        playerScript = GetComponent<PlayerScript>();

        if (GameObject.Find("SellText") != null)
        {
            sellText = GameObject.Find("SellText").GetComponent<TMP_Text>();
        }
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

    [Client]
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
            spawnTextPopUp(item,count);
 
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
        if (!isOwned) { return; }
        if (inventoryCount == null) { return; }
        if (inventoryFull == null) { return; }


        inventoryInspectorDisplay.Clear();
        foreach (InventoryItem item in inventoryItems)
        {
            inventoryInspectorDisplay.Add(item.item.name + " x" + item.count);
        }
        if (Input.GetKeyDown(inventoryKey))
        {
            ToggleInventory();
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
        UpdateInventoryDisplay();

        //if (sellText != null)
        //{
        //    sellText.SetText("Sell all items\n  $" + getSellValue());
        //}

    }

    public void ToggleInventory()
    {
        if (!canOpenInventory) return;

        inventoryDisplay.SetActive(!inventoryDisplay.activeSelf);
        //playerScript.setCanMove(!inventoryDisplay.activeSelf);
        //playerScript.setCanMine(!inventoryDisplay.activeSelf);
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

    public void sellAllItems()
    {
        //playerScript.addMoney(getSellValue());
        inventoryItems.Clear();
    }

    public int getSellValue()
    {
        int value = 0;
        foreach (InventoryItem item in inventoryItems)
        {
            value += item.item.value * item.count;
        }
        return value;
    }

    private void spawnTextPopUp(Item item, int count)
    {
        if (textPopUp == null) { return; }
        GameObject textPopUpInstance = Instantiate(textPopUp, transform.position, Quaternion.identity);
        textPopUpInstance.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        Color color;
        switch (item.name)
        {
            case "Gold":
                color = Color.yellow;
                break;

            case "Iron":
                color = new Color(.8f, .8f, .8f, 1); // Silverish
                break;

            case "Stone":
                color = new Color(.65f, .65f, .65f, 1); // Gray
                break;

            case "Diamond":
                color = new Color(.85f, .85f, 1, 1); // Light blue
                break;

            case "Wood":
                color = new Color(.6f, .4f, .04f, 1); // Brown
                break;

            default:
                color = Color.gray; // Default color
                break;
        }
        textPopUpInstance.GetComponent<textPopUp>().init(new Vector3(0, 10, -5), color, "added " + item.name + " x" + count, -.2f);
        Destroy(textPopUpInstance, 1f);
    }



}

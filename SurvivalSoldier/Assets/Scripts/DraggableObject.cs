using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DraggableObject : NetworkBehaviour
{
    [SyncVar] private GameObject playerDragging;
    [SerializeField] private int sellValue = 1;

    public void setBeingDragged(GameObject playerDragging)
    {
        this.playerDragging = playerDragging;
    }

    public void setSellValue(int sellValue)
    {
        this.sellValue = sellValue;
    }

    public bool getIsBeingDragged()
    {
        return this.playerDragging != null;
    }

    public int getSellValue()
    {
        return sellValue;
    }

    public GameObject getDragger()
    {
        return playerDragging;
    }
}

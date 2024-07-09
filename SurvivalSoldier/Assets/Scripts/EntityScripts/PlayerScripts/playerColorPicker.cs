using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerColorPicker : MonoBehaviour
{
    [SerializeField] private Material[] playerMaterials; // Assign materials for each player in the inspector

    public void SetMaterial(int playerNumber)
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null && playerMaterials.Length >= playerNumber)
        {
            renderer.material = playerMaterials[playerNumber];
        }
    }
}

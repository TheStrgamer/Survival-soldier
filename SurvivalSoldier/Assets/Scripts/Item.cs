using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public Sprite icon;
    public new string name;
    public int value;


    public Sprite GetSprite()
    {
        return icon;
    }
}
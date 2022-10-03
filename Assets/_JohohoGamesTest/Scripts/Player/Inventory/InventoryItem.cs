using UnityEngine;
using System;

[Serializable]
public class InventoryItem
{
    public GameObject Item;
    public ItemTypes Type;
    public InventoryItem(GameObject go, ItemTypes type)
    {
        Item = go;
        Type = type;
    }
}
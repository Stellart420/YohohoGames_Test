using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Inventory_Objects", menuName = "Data/InventoryObjects", order = 51)]
public class InventoryObjectsData : ScriptableObject
{
    [field: SerializeField] public List<InventoryItem> Items { get; private set; }
}
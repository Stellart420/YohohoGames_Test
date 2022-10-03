using UnityEngine.Pool;

public struct InventoryPool
{
    public ItemTypes Type;
    public IObjectPool<InventoryItem> Items;

    public InventoryPool(IObjectPool<InventoryItem> items, ItemTypes type)
    {
        Type = type;
        Items = items;
    }
}
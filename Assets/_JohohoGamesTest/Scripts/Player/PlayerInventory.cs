using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerInventory : MonoBehaviour, IInitializable
{
    [SerializeField] private InventoryObjectsData _data;
    [SerializeField] private Transform _inventoryPoint;
    [SerializeField] private float _inventoryItemSpace = 0.3f;

    private List<InventoryItem> _inventoryItems = new List<InventoryItem>();

    private int _itemsMax;
    private Player _player;

    private List<InventoryPool> _pools = new List<InventoryPool>();

    public Action<int> OnChangeItems;

    public void Init(Player player)
    {
        _player = player;
        _itemsMax = _player.Data.MaxInvetoryItems;
        _pools = new List<InventoryPool>();

        for (int i = 0; i < _data.Items.Count; i++)
        {
            var item = _data.Items[i];
            var pool = new ObjectPool<InventoryItem>(() => CreateItem(item), OnTakeItemFromPool, OnReturnedItemToPool, OnDestroyItem, false, _itemsMax, _itemsMax);

            var invPool = new InventoryPool(pool, _data.Items[i].Type);
            _pools.Add(invPool);
        }
    }

    public bool TryAddItem(ItemTypes type)
    {
        if (_itemsMax <= _inventoryItems.Count)
            return false;

        AddItem(type);
        OnChangeItems?.Invoke(_inventoryItems.Count);
        return true;
    }

    public bool TryRemoveItem(Transform point)
    {
        var inventoryItem = _inventoryItems.FirstOrDefault();

        if (inventoryItem == null)
            return false;

        return TryRemoveItem(inventoryItem.Type, point);
    }

    public bool TryRemoveItem(ItemTypes type, Transform point)
    {
        if (_inventoryItems.Count <= 0)
            return false;

        var inventoryItem = _inventoryItems.LastOrDefault(p => p.Type == type);

        if (inventoryItem == null)
            return false;

        _inventoryItems.Remove(inventoryItem);
        inventoryItem.Item.transform.DOScale(0, 1f);
        inventoryItem.Item.transform.DOMove(point.position, 1f).OnComplete(()=>
        {
            RemoveItem(inventoryItem);
        });
        OnChangeItems?.Invoke(_inventoryItems.Count);
        return true;
    }

    public void AddItem(ItemTypes type)
    {
        var pool = _pools.FirstOrDefault((p) => p.Type == type);
        var item = pool.Items.Get();

        _inventoryItems.Add(item);
        UpdateInventory();
    }

    public void RemoveItem(InventoryItem item)
    {
        var pool = _pools.FirstOrDefault((p) => p.Type == item.Type);
        pool.Items.Release(item);
        UpdateInventory();
    }

    void UpdateInventory()
    {
        OnChangeItems?.Invoke(_inventoryItems.Count);
        _inventoryItems = _inventoryItems.OrderBy(item => item.Type).ToList();
        for (int i = 0; i < _inventoryItems.Count; i++)
        {
            _inventoryItems[i].Item.transform.localPosition = Vector3.right * _inventoryItemSpace * i;
        }
    }

    InventoryItem CreateItem(InventoryItem inventory_object)
    {
        var item = Instantiate(inventory_object.Item, _inventoryPoint, false);
        var inventoryItem = new InventoryItem(item, inventory_object.Type);
        return inventoryItem;
    }

    void OnReturnedItemToPool(InventoryItem item)
    {
        item.Item.SetActive(false);
    }

    void OnTakeItemFromPool(InventoryItem item)
    {
        item.Item.transform.localScale = Vector3.one;
        item.Item.SetActive(true);
    }

    void OnDestroyItem(InventoryItem item)
    {
        Destroy(item.Item);
    }
}
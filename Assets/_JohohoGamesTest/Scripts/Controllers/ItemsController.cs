using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class ItemsController : ControllerBase
{
    [SerializeField] private List<Item> _items = new List<Item>();

    public List<PoolItem> ItemPools { get; private set; }

    public override void Init(MainController main)
    {
        base.Init(main);

        ItemPools = new List<PoolItem>();

        for (int i = 0; i < _items.Count; i++)
        {
            var item = _items[i];
            var pool = new ObjectPool<Item>(()=>CreateCurrency(item), OnTakeCurrencyFromPool, OnReturnedCurrencyToPool, OnDestroyCurrency, false);
            var poolItem = new PoolItem(item.Type, pool);
            ItemPools.Add(poolItem);
        }
    }

    public PoolItem GetPool(ItemTypes type)
    {
        return ItemPools.FirstOrDefault(pool => pool.Type == type);
    }

    Item CreateCurrency(Item go)
    {
        var item = Instantiate(go, transform, false);
        item.transform.localPosition = Vector3.zero;
        return item;
    }

    void OnReturnedCurrencyToPool(Item currency)
    {
        currency.transform.eulerAngles = Vector3.zero;
        currency.gameObject.SetActive(false);
    }

    void OnTakeCurrencyFromPool(Item currency)
    {
        currency.gameObject.SetActive(true);
    }

    void OnDestroyCurrency(Item currency)
    {
        Destroy(currency);
    }
}

public struct PoolItem
{
    public ItemTypes Type;
    public ObjectPool<Item> Pool;

    public PoolItem(ItemTypes type, ObjectPool<Item> pool)
    {
        Type = type;
        Pool = pool;
    }
}

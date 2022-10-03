using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemTypes _type;

    public ItemTypes Type => _type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (player.Inventory.TryAddItem(_type))
            {
                //todo Добавить Визуальный эффект подбора
                var itemPool = MainController.Instance.ItemsController.ItemPools.FirstOrDefault(p => p.Type == _type);
                itemPool.Pool.Release(this);
            }
        }
    }
}

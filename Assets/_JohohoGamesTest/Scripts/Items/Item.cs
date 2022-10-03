using System.Linq;
using UnityEngine;

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
                var itemPool = MainController.Instance.ItemsController.ItemPools.FirstOrDefault(p => p.Type == _type);
                itemPool.Pool.Release(this);
            }
        }
    }
}

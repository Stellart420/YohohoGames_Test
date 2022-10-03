using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private List<ItemTypes> _itemTypes = new List<ItemTypes>();
    [SerializeField] private float _frequency = 5;
    [SerializeField] private SpawnZone _spawnZone;
    [SerializeField] private int _maxItemsSpawned = 30;
    YieldInstruction _wait;

    private WorldController _worldController;

    private void Start()
    {
        _worldController = MainController.Instance.WorldController;
        _wait = new WaitForSeconds(_frequency);
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return _wait;

            if (transform.childCount >= _maxItemsSpawned)
                continue;

            var rand = Random.Range(0, _itemTypes.Count);
            var pool = MainController.Instance.ItemsController.GetPool(_itemTypes[rand]);
            var item = pool.Pool.Get();
            item.transform.SetParent(_worldController.ItemsParent);
            item.transform.position = _spawnZone.SpawnPoint + Vector3.up * 0.125f;
            item.transform.eulerAngles = new Vector3(0, Random.Range(0f, 360f), -90);
        }    
    }
}

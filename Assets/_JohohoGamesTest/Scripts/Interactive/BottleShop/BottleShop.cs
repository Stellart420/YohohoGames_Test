using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BottleShop : MonoBehaviour
{
    [SerializeField] private ShopData _data;
    [SerializeField] private Transform _fxPoint;
    [SerializeField] private float _timeBetweenSell = 0.5f;

    private static YieldInstruction _wait;
    private Player _currentPlayer;

    private void Awake()
    {
        _wait = new WaitForSeconds(_timeBetweenSell);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _currentPlayer = player;
            StartCoroutine(GetItem());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _currentPlayer = null;
        }
    }

    IEnumerator GetItem()
    {
        while (_currentPlayer != null)
        {
            for (int i = 0; i < _data.Items.Count; i++)
            {
                var needItem = _data.Items[i];
                if (_currentPlayer.Inventory.TryRemoveItem(needItem.Type, transform))
                {
                    //todo Ёффект вылетани€ монеток с магазина в UI
                    var currencyGO = MainController.Instance.CurrencyController.CurrencyPool.Get();
                    currencyGO.transform.SetParent(_fxPoint, false);
                    currencyGO.transform.localPosition = Vector3.zero;
                    currencyGO.transform.DOLocalMoveY(2.5f, .5f).OnComplete(() => MainController.Instance.CurrencyController.CurrencyPool.Release(currencyGO));
                    MainController.Instance.CurrencyController.Add(needItem.Currency);
                }
            }
            yield return _wait; 
        }
    }
}

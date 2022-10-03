using System;
using UnityEngine;
using UnityEngine.Pool;

public class CurrencyController : ControllerBase
{
    public ObjectPool<GameObject> CurrencyPool { get; private set; }

    [SerializeField] private GameObject _currencyGO;

    private float _currency;

    public float Currency
    {
        get { return _currency; }
        private set 
        { 
            _currency = value;

            if (_currency < 0)
                _currency = 0;

            OnChangeMoney?.Invoke(_currency); 
        }
    }

    public Action<float> OnChangeMoney;

    public void Add(float value)
    {
        Currency += value;
    }

    public void Remove(float value)
    {
        Currency -= value;
    }

    private void Awake()
    {
        CurrencyPool = new ObjectPool<GameObject>(CreateCurrency, OnTakeCurrencyFromPool, OnReturnedCurrencyToPool, OnDestroyCurrency, false);
    }

    GameObject CreateCurrency()
    {
        var item = Instantiate(_currencyGO, transform, false);
        item.transform.localPosition = Vector3.zero;
        return item;
    }

    void OnReturnedCurrencyToPool(GameObject currency)
    {
        currency.transform.localScale = Vector3.one;
        currency.SetActive(false);
    }

    void OnTakeCurrencyFromPool(GameObject currency)
    {
        currency.SetActive(true);
    }

    void OnDestroyCurrency(GameObject currency)
    {
        Destroy(currency);
    }
}

using DG.Tweening;
using UnityEngine;

public class StripState : BusinessState
{
    [SerializeField] private Trigger _trigger;

    [Header("Currency")]
    [SerializeField] private Transform _currencyPoint;
    [SerializeField] private float _income = 2;
    [SerializeField] private float _frequency = 5;

    [Space()]
    [SerializeField] private Unit _unit;

    private CurrencyController _currencyController;

    private float _wait;

    public override void Loop()
    {
        _wait += Time.deltaTime;

        if (_wait >= _frequency)
        {
            _currencyController.Add(_income);
            var currency = _currencyController.CurrencyPool.Get();
            currency.transform.SetParent(_currencyPoint, false);
            currency.transform.DOLocalMoveY(1, 0.2f).OnComplete(() => _currencyController.CurrencyPool.Release(currency));
            _wait = 0;
        }
    }

    public override void OnStateEnter()
    {
        transform.localScale = Vector3.zero;
        gameObject.SetActive(true);
        transform.DOScale(Vector3.one, 2f).OnComplete(()=>
        {
            _unit.transform.localScale = Vector3.one;
            _unit.gameObject.SetActive(true);
        });
        _currencyController = MainController.Instance.CurrencyController;
        _trigger.OnEnter += CheckTrigger;
    }

    public override void OnStateExit()
    {
        _trigger.OnEnter -= CheckTrigger;
        gameObject.SetActive(false);
    }

    private void CheckTrigger(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {

        }
    }
}

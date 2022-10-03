using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PrepareState : BusinessState
{
    [SerializeField] private Trigger _trigger;
    [SerializeField] private float _needCurrency = 10;
    [SerializeField] private float _betweenSpendTime = 1f;

    private CurrencyController _currencyController;
    private Player _currentPlayer;

    public override void Loop()
    {
        if (_needCurrency <= 0)
        {
            var machine = SM as BusinessStateMachine;
            machine.NextState();
        }
    }

    private void CheckEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _currentPlayer = player;
            StartCoroutine(TrySpend());
        }
    }

    private void CheckExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _currentPlayer = null;
        }
    }

    IEnumerator TrySpend()
    {
        while (_currentPlayer != null)
        {
            if (_currencyController.Currency <= 0)
            {
                yield return null;
                continue;
            }

            if (_needCurrency <= 0)
                break;

            var currencyGO = _currencyController.CurrencyPool.Get();
            currencyGO.transform.position = _currentPlayer.Inventory.transform.position + Vector3.up * 2.5f;
            currencyGO.transform.DOScale(.2f, .5f);
            currencyGO.transform.DOMove(transform.position, .5f).OnComplete(()=>_currencyController.CurrencyPool.Release(currencyGO));
            _currencyController.Remove(1f);
            _needCurrency --;

            yield return new WaitForSeconds(_betweenSpendTime);
        }
    }

    public override void OnStateEnter()
    {
        gameObject.transform.localScale = Vector3.zero;
        gameObject.SetActive(true);
        gameObject.transform.DOScale(1f, 2f);
        _currencyController = MainController.Instance.CurrencyController;
        _trigger.OnEnter += CheckEnter;
        _trigger.OnExit += CheckExit;
    }

    public override void OnStateExit()
    {
        _trigger.OnEnter -= CheckEnter;
        _trigger.OnExit -= CheckExit;
        _trigger.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}

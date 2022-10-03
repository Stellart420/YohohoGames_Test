using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePanel : PanelBase
{
    [field: SerializeField] public FloatingJoystick Joystick { get; private set; }
    [SerializeField] private TextMeshProUGUI _currency;
    [SerializeField] private TextMeshProUGUI _items;

    public void Init()
    {
        MainController.Instance.CurrencyController.OnChangeMoney += SetCurrency;
        MainController.Instance.PlayerController.Player.Inventory.OnChangeItems += SetItems;
    }

    private void SetCurrency(float value)
    {
        _currency.text = $"{value}";
    }

    private void SetItems(int value)
    {
        _items.text = $"{value}";
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanelBase : MonoBehaviour
{
    [SerializeField] protected CanvasGroup _panel;
    internal bool _isShown;


    public virtual void Show()
    {
        _panel.gameObject.SetActive(true);
        _panel.DOFade(1f, 0.2f);
        _isShown = true;
    }

    public virtual void Hide()
    {
        _panel.DOFade(0f, 0.2f);
        _panel.gameObject.SetActive(false);
        _isShown = false;
    }
}

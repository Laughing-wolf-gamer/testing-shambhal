using System;
using DG.Tweening;
using UnityEngine;

public class UibuttonAnimation : MonoBehaviour
{
    [SerializeField] private Vector2 endValueButton;
    [SerializeField] private float endValueDuration = 1;
    [SerializeField] private Ease effectEase = Ease.InOutElastic;
    [SerializeField] private RectTransform logInButton;
    private Vector2 startValue;
    private void Awake()
    {
        logInButton = GetComponent<RectTransform>();
        startValue = logInButton.sizeDelta;
    }
    public void OnClickAnimation()
    {
        logInButton.DOSizeDelta(endValueButton, endValueDuration, false).SetEase(effectEase).onComplete += () =>
        {
            logInButton.DOSizeDelta(startValue, endValueDuration, false);
        };
    }
    public void OnClickEvent(Action OnAnimationComplete)
    {
        logInButton.DOSizeDelta(endValueButton, endValueDuration, false).SetEase(effectEase).onComplete += () =>
        {
            logInButton.DOSizeDelta(startValue, endValueDuration, false).SetEase(Ease.InSine).onComplete += () =>
            {
                OnAnimationComplete?.Invoke();
            };
        };
    }
}

using UnityEngine;
using UnityEngine.UI;
using Abhishek.Utils;
using DG.Tweening;
using System;

[RequireComponent(typeof(UIInputReciver), typeof(UIInputHandler))]
public class UIButtonCustom : Button
{
    private UibuttonAnimation uibuttonAnimation;
    private UIInputReciver reciver;
    [SerializeField] private bool useAnimations;
    [SerializeField] private Vector2 endValueButton;
    [SerializeField] private float endValueDuration = 1;
    [SerializeField] private Ease effectEase = Ease.InOutElastic;
    [SerializeField] private RectTransform logInButton;
    private Vector2 startValue;
    protected override void Start()
    {
        base.Start();
        logInButton = GetComponent<RectTransform>();
        startValue = logInButton.sizeDelta;
    }
    protected override void Awake()
    {
        uibuttonAnimation = GetComponent<UibuttonAnimation>();
        base.Awake();
        reciver = GetComponent<UIInputReciver>();
        onClick.AddListener(() =>
        {
            if (useAnimations)
            {
                AudioManager.Instance?.PlayOneShotMusic(Sounds.SoundType.btnClick);
                OnClickAnimation(() =>
                {
                    reciver.OnInputRecives();
                });
            }
            else
            {
                reciver.OnInputRecives();
                AudioManager.Instance?.PlayOneShotMusic(Sounds.SoundType.btnClick);
            }
        });
    }
    public void OnClickAnimation(Action action)
    {
        logInButton.DOSizeDelta(endValueButton,endValueDuration,false).SetEase(effectEase).onComplete += ()=>
        {
            logInButton.DOSizeDelta(startValue, endValueDuration, false);
            action?.Invoke();

        };
    }
    
}

using System;
using Abhishek.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class Coin : MonoBehaviour,IPooledObject,IPointerDownHandler,IPointerUpHandler {
    [SerializeField] private float maxLiveTime;
    [SerializeField] private Image selfCanvasGroup;
    [SerializeField] private Image coinDisappearTimerImage;
    [SerializeField] private RectTransform coinRect;
    
    private float currentLiveTimer;
    private Transform orignalTransform;
    private void Start()
    {
        currentLiveTimer = maxLiveTime;
    }
    public void DestroyMySelfWithDelay(float delay = 0)
    {

    }

    public void DestroyNow()
    {
        transform.SetParent(orignalTransform);
        gameObject.SetActive(false);
    }
    private void Update() {
        if (MasterController.Instance != null)
        {
            if (MasterController.Instance.IsGamePlaying() && !MasterController.Instance.IsGamePaused())
            {
                if (currentLiveTimer > 0)
                {
                    currentLiveTimer -= Time.deltaTime;
                    coinDisappearTimerImage.fillAmount = currentLiveTimer / maxLiveTime;
                }
                else
                {
                    DestroyNow();
                }
            }
        }
    }
    public void SetOrignalParent(Transform orignalTransform)
    {
        this.orignalTransform = orignalTransform;
    }
    public void OnObjectReuse()
    {
        gameObject.SetActive(true);
        currentLiveTimer = maxLiveTime;
        selfCanvasGroup.raycastTarget = true;
        coinRect.sizeDelta = Vector2.zero;
        coinRect.DOSizeDelta(new Vector2(100, 100), 0.1f, false).SetEase(Ease.OutSine);
    }

    public void setPosition(RectTransform gameBoudiry, Vector2 spawnPosition)
    {
        coinRect.SetParent(gameBoudiry, false);
        coinRect.anchoredPosition = spawnPosition;
        Debug.Log("Coin Spawning: ");
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        if (MasterController.Instance.IsGamePaused())
        {
            return;
        }
        AudioManager.Instance?.PlayOneShotMusic(Sounds.SoundType.CoinClicked);
        selfCanvasGroup.raycastTarget = false;
        MasterController.Instance?.CoinClicked();
        coinRect.DOSizeDelta(Vector2.zero, 0.1f, false).SetEase(Ease.InSine).onComplete += ()=>
        {
            currentLiveTimer = 0;
            DestroyNow();
        };
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer Down");
    }
}

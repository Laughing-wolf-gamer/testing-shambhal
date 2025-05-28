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
    private Transform originalTransform;
	private MasterController masterController;
    private void Start()
	{
		masterController = MasterController.Instance;
		currentLiveTimer = maxLiveTime;
	}
    public void DestroyMySelfWithDelay(float delay = 0){}

    public void DestroyNow()
    {
		masterController?.OnCoinCollect(this);
        transform.SetParent(originalTransform);
        gameObject.SetActive(false);
    }
    private void Update() {
        if (masterController != null)
        {
            if (masterController.IsGamePlaying() && !masterController.IsGamePaused())
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
        this.originalTransform = orignalTransform;
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
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        if (masterController.IsGamePaused())
        {
            return;
        }
        AudioManager.Instance?.PlayOneShotMusic(Sounds.SoundType.CoinClicked);
        selfCanvasGroup.raycastTarget = false;
        masterController?.CoinClicked();
        coinRect.DOSizeDelta(Vector2.zero, 0.1f, false).SetEase(Ease.InSine).onComplete += ()=>
        {
#if UNITY_ANDROID
			Handheld.Vibrate();
#endif
            currentLiveTimer = 0;
            DestroyNow();
        };
    }

    public void OnPointerDown(PointerEventData eventData) {}
}

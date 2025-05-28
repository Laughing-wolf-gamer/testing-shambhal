using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MasterController : MonoBehaviour
{
    [SerializeField] private GameDataSO gameData;
    [SerializeField] private UnityEvent OnStartEvent;
    [SerializeField] private UnityEvent OnGameStartEvent;
    [SerializeField] private UnityEvent OnEndEvent;
    [SerializeField] private UnityEvent OnPauseGameUI;
    [SerializeField] private UnityEvent OnResumeGameUI;
    [SerializeField] private float maxGametime = 30f;
    private float currentGameTime = 0;

    [SerializeField] private bool isPlaying = false;
    [SerializeField] private bool isPaused = false;


    public Action OnPause, OnResume;
    public Action<int> OnCoinClicked;
	public Action<Coin> OnCoinCollected;
    public Action<float> OnTimerTick;
    private int totalCoinsCollected;
    public static MasterController Instance{ get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        StartCoroutine(StartRoutine());
    }
    private void Update()
    {
        if (isPlaying && !isPaused)
        {
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				if (isPaused)
				{
					ResumeGamePause();
				}
				else
				{
					GamePause();
				}
			}
            if (currentGameTime > 0)
			{
				currentGameTime -= Time.deltaTime;
				OnTimerTick?.Invoke(currentGameTime);

			}
			else
			{
				isPlaying = false;
			}
        }
    }
    private IEnumerator StartRoutine()
    {
        OnStartEvent?.Invoke();
        while (!isPlaying)
        {
            yield return null;
        }
        StartCoroutine(GamePlayingRoutine());
    }
    private IEnumerator GamePlayingRoutine()
    {
        OnGameStartEvent?.Invoke();
        while (isPlaying)
        {

            yield return null;
        }
        gameData.SetHighScore(totalCoinsCollected);
        OnEndEvent?.Invoke();

    }
    public void StartGame()
    {
        currentGameTime = maxGametime;
        Debug.Log("Game Start: ");
        isPlaying = true;
    }

    public void GamePause()
    {
        if (!isPlaying)
        {
            return;
        }
        isPaused = true;
        OnPauseGameUI?.Invoke();
        OnPause?.Invoke();
    }
    public void ResumeGamePause()
    {
        if (!isPlaying)
        {
            return;
        }
        isPaused = false;
        OnResumeGameUI?.Invoke();
        OnResume?.Invoke();
    }
    public void CoinClicked()
    {
        totalCoinsCollected++;
        OnCoinClicked?.Invoke(totalCoinsCollected);
    }
	public void OnCoinCollect(Coin coin)
	{
		OnCoinCollected?.Invoke(coin);
	}
    public bool IsGamePlaying()
    {
        return isPlaying;
    }

    public bool IsGamePaused()
    {
        return isPaused;
    }
    public int GetCurrentHighScore()
    {
        return totalCoinsCollected;
    }
}

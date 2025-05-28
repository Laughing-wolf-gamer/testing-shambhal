using System;
using Abhishek.Utils;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour {
	[SerializeField] private RectTransform endScreenWindow;
	[SerializeField] private GameDataSO gameData;
	[SerializeField] private TextMeshProUGUI coinCountTextUi;
	[SerializeField] private TextMeshProUGUI timerTextUi;


	[SerializeField] private TextMeshProUGUI currentScore, highScore;
	[SerializeField] private MasterController masterController;


	private void Start()
	{
		masterController.OnCoinClicked += MasterController_Instance_OnCoinClicked;
		masterController.OnTimerTick += MasterController_Instance_OnTimerTick;
	}
	void OnDestroy()
	{
		masterController.OnCoinClicked -= MasterController_Instance_OnCoinClicked;
		masterController.OnTimerTick -= MasterController_Instance_OnTimerTick;
	}

	private void MasterController_Instance_OnCoinClicked(int count)
	{
		coinCountTextUi.SetText(count.ToString());
	}
	public void MasterController_Instance_OnTimerTick(float timer)
	{
		if (timer <= 5f)
		{
			timerTextUi.color = Color.red;
		}
		timerTextUi.SetText(string.Concat("Time Left:", Mathf.CeilToInt(timer), "s"));
	}
	public void ShowHighScore()
	{
		currentScore.SetText(MasterController.Instance.GetCurrentHighScore().ToString());
		highScore.SetText(gameData.GetHighestScore().ToString());
	}

	public void RestartGame()
	{
		ObjectPoolingManager.ResetPools(null);
		LevelLoader.Instance.PlayNextLevel(SceneIndex.GameScene);
	}
	public void MoveToMenu()
	{
		ObjectPoolingManager.ResetPools(null);
		LevelLoader.Instance.PlayNextLevel(SceneIndex.MainMenu);
	}
	public void ShowwEndScreen()
	{
		endScreenWindow.DOAnchorPosY(0, 0.5f).SetEase(Ease.OutBack).SetDelay(.5f);
	}
}

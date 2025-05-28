using System;
using Abhishek.Utils;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
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
        timerTextUi.SetText(Mathf.CeilToInt(timer).ToString());
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
}

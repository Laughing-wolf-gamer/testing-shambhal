using System.Collections.Generic;
using Abhishek.Utils;
using UnityEngine;

public class CoinSpawnControler : MonoBehaviour
{
    [SerializeField] private RectTransform gameBoudiry;
    [SerializeField] private Vector2 coinSizeDelta;

    [SerializeField] private MasterController masterController;
    [SerializeField] private float maxSpawnDelay = 2;
	[SerializeField] private List<RectTransform> spawnedCoins = new List<RectTransform>(); // Track spawned coins
    private float currentSpawnTime;
	private void Start() {
		masterController.OnCoinCollected += MasterController_OnCoinClicked;
	}
	private void OnDestroy() {
		masterController.OnCoinCollected -= MasterController_OnCoinClicked;
	}
	

	private void MasterController_OnCoinClicked(Coin coin)
	{
		if (coin.TryGetComponent(out RectTransform coinRect))
		{
			if (spawnedCoins.Contains(coinRect))
			{
				spawnedCoins.Remove(coinRect);
			}
		}
	}

	private void Update()
	{
		if (masterController.IsGamePlaying())
		{
			if (currentSpawnTime > 0)
			{
				currentSpawnTime -= Time.deltaTime;
			}
			else
			{
				SpawnCoin();
			}
		}
	}

    public void SpawnCoin() {
        Vector2 canvasSize = gameBoudiry.sizeDelta;
		Vector2 coinSize = coinSizeDelta;

		float minX = -canvasSize.x / 2 + coinSize.x / 2;
		float maxX = canvasSize.x / 2 - coinSize.x / 2;
		float minY = -canvasSize.y / 2 + coinSize.y / 2;
		float maxY = canvasSize.y / 2 - coinSize.y / 2;

		const int maxAttempts = 50;
		int attempts = 0;
		Vector2 spawnPosition;
		bool validPosition = false;

		do
		{
			float randX = Random.Range(minX, maxX);
			float randY = Random.Range(minY, maxY);
			spawnPosition = new Vector2(randX, randY);
			validPosition = true;

			foreach (RectTransform existingCoin in spawnedCoins)
			{
				if (Vector2.Distance(existingCoin.anchoredPosition, spawnPosition) < coinSize.x)
				{
					validPosition = false;
					break;
				}
			}

			attempts++;
		} while (!validPosition && attempts < maxAttempts);

		if (!validPosition)
		{
			Debug.LogError("Couldn't find a non-overlapping position for coin.");
			return;
		}

		GameObject coin = ObjectPoolingManager.SpawnFromPool("Coin", Vector3.zero, Quaternion.identity);

		if (coin.TryGetComponent<Coin>(out Coin coinRect)) {
			coinRect.setPosition(gameBoudiry, spawnPosition);
			if (coin.TryGetComponent(out RectTransform rectTransform)){
				spawnedCoins.Add(rectTransform);				
			}
		}

		currentSpawnTime = Random.Range(1f, maxSpawnDelay + 0.6f);
    }
}

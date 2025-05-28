using Abhishek.Utils;
using UnityEngine;

public class CoinSpawnControler : MonoBehaviour
{
    [SerializeField] private RectTransform gameBoudiry;
    [SerializeField] private Vector2 coinSizeDelta;

    [SerializeField] private MasterController masterController;
    [SerializeField] private float maxSpawnDelay = 2;
    private float currentSpawnTime;
    private void Update() {
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

    public void SpawnCoin()
    {
        // Get canvas dimensions
        Vector2 canvasSize = gameBoudiry.sizeDelta;
        Vector2 coinSize = coinSizeDelta;

        // Calculate bounds to keep the coin fully visible
        float minX = -canvasSize.x / 2 + coinSize.x / 2;
        float maxX = canvasSize.x / 2 - coinSize.x / 2;
        float minY = -canvasSize.y / 2 + coinSize.y / 2;
        float maxY = canvasSize.y / 2 - coinSize.y / 2;

        float randX = Random.Range(minX, maxX);
        float randY = Random.Range(minY, maxY);

        Vector2 spawnPosition = new Vector2(randX, randY);

        // Spawn coin from pool
        GameObject coin = ObjectPoolingManager.SpawnFromPool("Coin", Vector3.zero, Quaternion.identity);

        // Set the position in UI space
        if (coin.TryGetComponent<Coin>(out Coin coinRect))
        {
            coinRect.setPosition(gameBoudiry, spawnPosition);
        }
        currentSpawnTime = UnityEngine.Random.Range(1,maxSpawnDelay + .6f);
    }
}

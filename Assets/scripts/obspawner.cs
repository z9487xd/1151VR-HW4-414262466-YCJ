using UnityEngine;

public class obspawner : MonoBehaviour
{
    [Header("Base Settings")]
    public GameObject obstaclePrefab;
    public Transform playerTransform;

    [Header("Distance Settings (X Axis)")]
    public float spawnIntervalDistance = 20f;
    public int initialObstaclesCount = 5;

    [Header("Random Left/Right Settings (Z Axis)")]
    public float minZ = -5f;
    public float maxZ = 5f;

    [Header("Random Up/Down Settings (Y Axis)")]
    public float minY = -2f;
    public float maxY = 6f;

    [Header("Sensor Settings")]
    public float obstacleHalfSize = 5f;   // 方塊半徑，邊長 10 就填 5
    public float planeMargin = 0.5f;

    private float lastSpawnX;

    void Start()
    {
        if (playerTransform == null) return;

        lastSpawnX = playerTransform.position.x;

        for (int i = 1; i <= initialObstaclesCount; i++)
        {
            float spawnX = lastSpawnX + (i * spawnIntervalDistance);
            SpawnObstacleAtX(spawnX);
            lastSpawnX = spawnX;
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        if (playerTransform.position.x > lastSpawnX - (spawnIntervalDistance * initialObstaclesCount))
        {
            float nextSpawnX = lastSpawnX + spawnIntervalDistance;
            SpawnObstacleAtX(nextSpawnX);
            lastSpawnX = nextSpawnX;
        }
    }

    void SpawnObstacleAtX(float xPosition)
    {
        float randomY = Random.Range(minY, maxY);
        float randomZ = Random.Range(minZ, maxZ);
        Vector3 spawnPosition = new Vector3(xPosition, randomY, randomZ);

        GameObject obj = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);

        // 自動加上判定腳本，不用手動拖到 Prefab
        ObstacleSensor sensor = obj.AddComponent<ObstacleSensor>();
        sensor.halfSize = obstacleHalfSize;
        sensor.planeMargin = planeMargin;
    }
}
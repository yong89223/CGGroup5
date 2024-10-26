using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // 생성할 적 프리팹
    public float spawnInterval = 5f; // 적 생성 주기
    public Vector3 mapSize = new Vector3(28, 0, 28); // 맵의 크기 (X, Z 축 크기)
    private float spawnTimer; // 생성 타이머

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f; // 타이머 초기화
        }
    }

    private void SpawnEnemy()
    {
        // 랜덤 위치 지정 (플레인 맵 위에서만 생성되도록)
        float spawnX = Random.Range(-mapSize.x / 2, mapSize.x / 2);
        float spawnZ = Random.Range(-mapSize.z / 2, mapSize.z / 2);
        Vector3 spawnPosition = new Vector3(spawnX, 1, spawnZ);

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
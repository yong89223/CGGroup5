using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // ������ �� ������
    public float spawnInterval = 5f; // �� ���� �ֱ�
    public Vector3 mapSize = new Vector3(28, 0, 28); // ���� ũ�� (X, Z �� ũ��)
    private float spawnTimer; // ���� Ÿ�̸�

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f; // Ÿ�̸� �ʱ�ȭ
        }
    }

    private void SpawnEnemy()
    {
        // ���� ��ġ ���� (�÷��� �� �������� �����ǵ���)
        float spawnX = Random.Range(-mapSize.x / 2, mapSize.x / 2);
        float spawnZ = Random.Range(-mapSize.z / 2, mapSize.z / 2);
        Vector3 spawnPosition = new Vector3(spawnX, 1, spawnZ);

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
using UnityEngine;

public class HealthItemSpawner : MonoBehaviour
{
    public GameObject healthItemPrefab; // 체력 아이템 프리팹
    public Transform[] spawnLocations; // 아이템 생성 위치 배열
    public float spawnInterval = 20f; // 생성 간격 (초)

    void Start()
    {
        // 일정 시간마다 아이템 생성
        InvokeRepeating(nameof(SpawnHealthItem), spawnInterval, spawnInterval);
    }

    void SpawnHealthItem()
    {
        // 랜덤한 위치에 아이템 생성
        Transform randomLocation = spawnLocations[Random.Range(0, spawnLocations.Length)];
        Instantiate(healthItemPrefab, randomLocation.position, Quaternion.identity);
    }
}
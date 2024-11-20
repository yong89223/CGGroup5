using UnityEngine;
using UnityEngine.AI;

public class ObstacleSetup : MonoBehaviour
{
    void Start()
    {
        NavMeshObstacle obstacle = GetComponent<NavMeshObstacle>();
        if (obstacle != null)
        {
            obstacle.carving = true; // 실시간 경로 차단 활성화
        }
    }
}
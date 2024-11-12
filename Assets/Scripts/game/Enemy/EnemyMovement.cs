using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public string playerTag = "Player";  // 플레이어 오브젝트의 태그 설정
    public float stoppingDistance = 5.0f;  // 플레이어와의 최소 거리 유지

    private Transform player;
    private NavMeshAgent agent;

    void Start()
    {
        // NavMeshAgent 컴포넌트를 가져옴
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stoppingDistance;

        // 태그로 플레이어 오브젝트를 찾아 참조
        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player 오브젝트를 찾을 수 없습니다.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);  // 매 프레임마다 현재 플레이어 위치로 이동
        }
    }
}

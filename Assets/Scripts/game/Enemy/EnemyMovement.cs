using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public string playerTag = "Player";  // 플레이어 오브젝트의 태그 설정
    public float stoppingDistance = 5.0f;  // 플레이어와의 최소 거리 유지
    public Animator animator;

    private Transform player;
    private NavMeshAgent agent;
    private bool isFrozen = false; // 사격 중지 상태 확인

    public float repathInterval = 0.5f; // 경로 재탐색 주기
    private float nextRepathTime = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        // NavMeshAgent 설정
        agent.stoppingDistance = stoppingDistance;
        agent.updatePosition = true;  // NavMeshAgent가 위치를 직접 관리
        agent.updateRotation = true; // 회전도 NavMeshAgent에 의해 관리

        // 플레이어 참조
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
        if (isFrozen)
        {
            StopMovement();
            return;
        }

        if (player != null)
        {
            // 경로 재탐색
            if (Time.time >= nextRepathTime)
            {
                agent.SetDestination(player.position);
                nextRepathTime = Time.time + repathInterval;
            }

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer > stoppingDistance && agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                MoveTowardsPlayer();
            }
            else if (distanceToPlayer <= stoppingDistance)
            {
                StopMovement();
            }
        }
    }

    void StopMovement()
    {
        animator.SetBool("isRunning", false);
        agent.isStopped = true;  // NavMeshAgent 정지
        agent.velocity = Vector3.zero;  // 이동 속도 0으로 설정
    }

    void MoveTowardsPlayer()
    {
        animator.SetBool("isRunning", true);
        agent.isStopped = false; // NavMeshAgent 이동 활성화
    }

    // 사격 중지 상태 설정 메서드
    public void FreezeMovement(float duration)
    {
        isFrozen = true;
        StopMovement();
        Invoke(nameof(UnfreezeMovement), duration);
    }

    private void UnfreezeMovement()
    {
        isFrozen = false;
        agent.isStopped = false;
    }
}

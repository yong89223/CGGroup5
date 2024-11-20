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

    void Start()
    {
        animator = GetComponent<Animator>();
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
        if (isFrozen)
        {
            // 사격 중지 상태일 때 이동 비활성화
            agent.isStopped = true;
            animator.SetBool("isRunning", false);
            return;
        }

        if (player != null)
        {
            // 플레이어 위치로 이동 설정
            agent.SetDestination(player.position);

            // 플레이어와의 거리 계산
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // 애니메이션 상태 전환
            if (distanceToPlayer > stoppingDistance)
            {
                // 이동 중 (Run 애니메이션)
                animator.SetBool("isRunning", true);
                agent.isStopped = false;
            }
            else
            {
                // 멈춤 (Idle 애니메이션)
                animator.SetBool("isRunning", false);
                agent.isStopped = true;
            }
        }
    }

    // 사격 중지 상태 설정 메서드
    public void FreezeMovement(float duration)
    {
        isFrozen = true;
        Invoke(nameof(UnfreezeMovement), duration);
    }

    private void UnfreezeMovement()
    {
        isFrozen = false;
    }
}

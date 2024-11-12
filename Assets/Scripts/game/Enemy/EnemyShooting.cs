using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public string playerTag = "Player";   // 플레이어 태그
    public float shootRange = 10f;        // 사격 시작 거리
    public LayerMask obstacleLayer;       // 장애물 레이어
    public GameObject bulletPrefab;       // 총알 프리팹
    public Transform bulletSpawnPoint;    // 총알의 생성 위치
    public float fireRate = 1f;           // 사격 간격

    private Transform player;             // 플레이어의 Transform
    private bool canShoot = false;        // 플레이어를 쏠 수 있는 상태
    private float nextFireTime = 0f;

    void Start()
    {
        // 태그로 플레이어 오브젝트를 찾아서 Transform 할당
        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player 오브젝트를 찾을 수 없습니다. 태그를 확인하세요.");
        }
    }

    void Update()
    {
        if (player != null && IsInShootingPosition() && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    bool IsInShootingPosition()
    {
        if (player == null) return false;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 사격 범위 내에 있고 장애물이 없는지 확인
        if (distanceToPlayer <= shootRange)
        {
            RaycastHit hit;
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            // 장애물 감지를 위해 레이캐스트 사용
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, shootRange, obstacleLayer))
            {
                // 레이가 장애물에 닿으면 사격하지 않음
                canShoot = false;
            }
            else
            {
                // 명확한 시야와 사거리 내에 있으면 사격 가능
                canShoot = true;
            }
        }
        else
        {
            canShoot = false;
        }

        return canShoot;
    }

    void Shoot()
    {
        if (canShoot)
        {
            // 플레이어의 현재 위치를 가져오되 Y 값을 고정
            Vector3 targetPosition = player.position;
            targetPosition.y = bulletSpawnPoint.position.y;  // 총알의 Y 값을 고정하여 발사 높이 일관성 유지

            // 총알 생성 및 방향 설정
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

            // 플레이어의 현재 위치로 향하는 방향 계산
            Vector3 direction = (targetPosition - bulletSpawnPoint.position).normalized;

            // 총알의 Rigidbody에 속도 적용
            bullet.GetComponent<Rigidbody>().velocity = direction * 10f;

            // 총알이 회전하도록 방향 설정
            bullet.transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}

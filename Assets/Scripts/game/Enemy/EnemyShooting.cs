using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public string playerTag = "Player";   // 플레이어 태그
    public float shootRange = 5f;        // 사격 시작 거리
    public GameObject bulletPrefab;       // 총알 프리팹
    public Transform[] bulletSpawnPoints; // 총알의 생성 위치 배열
    public float fireRate = 2f;          // 사격 간격
    public Animator animator;
    private Transform player;            // 플레이어의 Transform
    private float nextFireTime = 0f;
    private EnemyMovement enemyMovement; // EnemyMovement 스크립트 참조

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
            Debug.LogError("Player 오브젝트를 찾을 수 없습니다.");
        }

        // Animator 및 EnemyMovement 컴포넌트 가져오기
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
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

        return distanceToPlayer <= shootRange;
    }

    void Shoot()
    {
        animator.SetTrigger("shootTrigger");

        foreach (Transform spawnPoint in bulletSpawnPoints)
        {
            GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
            Vector3 direction = (player.position - spawnPoint.position).normalized;
            direction.y = 0f; // Y값 고정

            // 총알에 속도 적용
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = direction * 10f;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

            // 적과 총알 충돌 무시
            Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>());

            // 총알 제거 타이머
            Destroy(bullet, 5f); // 5초 후 제거
        }

        enemyMovement.FreezeMovement(2f);
    }

}

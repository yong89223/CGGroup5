using UnityEngine;

public class BulletShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;

    private void OnEnable()
    {
        PlayerShooting.OnShoot += SpawnBullet;
    }

    private void OnDisable()
    {
        PlayerShooting.OnShoot -= SpawnBullet;
    }

    private void SpawnBullet(Vector3 direction)
    {
        if (bulletPrefab != null)
        {
            // 탄환을 고정된 Y값 (예: 0.5)에서 생성
            Vector3 spawnPosition = new Vector3(transform.position.x, 0.5f, transform.position.z);

            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

            bulletRb.useGravity = false; // 중력 영향 제거

            // 발사 방향에서 Y축 제거하고 일정한 높이에서만 이동
            direction.y = 0;
            bulletRb.velocity = direction.normalized * bulletSpeed;
        }
    }
}

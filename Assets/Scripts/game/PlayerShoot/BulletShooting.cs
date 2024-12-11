using UnityEngine;

public class BulletShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public Transform bulletSpawnPoints;
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
            // 발사 위치에서 총알 생성
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoints.position, Quaternion.identity);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

            if (bulletRb != null)
            {
                bulletRb.useGravity = false; // 중력 제거
                bulletRb.velocity = direction.normalized * bulletSpeed; // 속도 설정
            }
        }
    }
}

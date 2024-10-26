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
            // źȯ�� ������ Y�� (��: 0.5)���� ����
            Vector3 spawnPosition = new Vector3(transform.position.x, 0.5f, transform.position.z);

            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

            bulletRb.useGravity = false; // �߷� ���� ����

            // �߻� ���⿡�� Y�� �����ϰ� ������ ���̿����� �̵�
            direction.y = 0;
            bulletRb.velocity = direction.normalized * bulletSpeed;
        }
    }
}

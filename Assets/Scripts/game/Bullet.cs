using UnityEngine;
using System;
public class Bullet : MonoBehaviour
{
    public int damage = 10;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Detected with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Bullet hit the wall.");
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);  // 데미지 적용
            }

            Destroy(gameObject);  // 총알 삭제
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Bullet hit the enemy."); // 확인용 로그

            // 적에게 데미지 적용
            Health enemyHealth = collision.gameObject.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage); // 적에게 데미지 입힘
            }

            Destroy(gameObject); // 탄환 파괴
        }
    }
}
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    public int basePlayerDamage = 20; // 기본 데미지
    private int playerDamage;
    public int damage = 10; // 벽이나 플레이어를 때리는 데미지

    private void Start()
    {
        // DataManager를 활용하여 playerDamage 초기화
        var equippedItem = DataManager.instance.nowPlayer.inventory.items.Find(item => item.id == DataManager.instance.nowPlayer.item);
        int itemBonusDamage = equippedItem != null ? equippedItem.damage : 0;
        playerDamage = basePlayerDamage + itemBonusDamage;
    }

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
                playerHealth.TakeDamage(damage); // 데미지 적용
            }

            Destroy(gameObject); // 총알 삭제
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Bullet hit the enemy."); // 확인용 로그

            // 적에게 데미지 적용
            Health enemyHealth = collision.gameObject.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(playerDamage); // 적에게 데미지 입힘
            }

            Destroy(gameObject); // 탄환 파괴
        }
    }
}

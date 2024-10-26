using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public static event Action OnPlayerDeath; // 플레이어 사망 이벤트
    public event Action<int> OnHealthChanged; // 체력 변화 이벤트
    public event Action OnEnemyDeath; // 적 사망 이벤트

    private void Start()
    {
        currentHealth = maxHealth;
    }

    // 데미지 처리 함수
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        OnHealthChanged?.Invoke(currentHealth); // 체력 변화 이벤트 호출

        if (currentHealth <= 0)
        {
            if (CompareTag("Player"))
            {
                OnPlayerDeath?.Invoke(); // 플레이어 사망 이벤트 호출
            }
            else if (CompareTag("Enemy"))
            {
                OnEnemyDeath?.Invoke(); // 적 사망 이벤트 호출
                Destroy(gameObject); // 적 오브젝트 파괴
            }
        }
    }
}

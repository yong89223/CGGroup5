using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public int baseHealth = 100; // 기본 체력
    public int currentHealth; // 현재 체력
    public bool isPlayer = false; // 플레이어인지 여부

    // 이벤트 선언
    public static event Action OnPlayerDeath; // 플레이어 사망 이벤트
    public static event Action<int> OnPlayerHealthChanged; // 플레이어 체력 변화 이벤트

    public event Action OnEnemyDeath; // 적 사망 이벤트
    public event Action<int> OnEnemyHealthChanged; // 적 체력 변화 이벤트

    void Start()
    {
        InitializeHealth();
    }

    // 체력 초기화
    public void InitializeHealth() 
    {
        if (isPlayer)
        {
            // 플레이어: 기본 체력 + 장비 보너스 체력
            var equippedItem = DataManager.instance.nowPlayer.inventory.items.Find(item => item.id == DataManager.instance.nowPlayer.item);
            int itemBonusHp = equippedItem != null ? equippedItem.hp : 0;
            currentHealth = baseHealth + itemBonusHp;
            baseHealth = baseHealth + itemBonusHp;
        }
        else
        {
            // 적: 기본 체력 + 스테이지 인덱스 * 10
            currentHealth = baseHealth + DataManager.instance.nowPlayer.chapterIndex * 10;
            baseHealth = baseHealth + DataManager.instance.nowPlayer.chapterIndex * 10;
        }

        // 초기 체력 변화 이벤트 호출
        TriggerHealthChangedEvent();
    }

    // 데미지 처리
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        TriggerHealthChangedEvent();

       
    }

    public void IncreaseHealth(int increasing)
    {
        if (currentHealth + increasing < baseHealth)
            currentHealth += increasing;
        else
            currentHealth = baseHealth;
        TriggerHealthChangedEvent();

        if (currentHealth <= 0)
        {
            HandleDeath();
        }
    }

    // 체력 변화 이벤트 호출
    private void TriggerHealthChangedEvent()
    {
        if (isPlayer)
        {
            OnPlayerHealthChanged?.Invoke(currentHealth);
        }
        else
        {
            OnEnemyHealthChanged?.Invoke(currentHealth);
        }
    }

    // 사망 처리
    private void HandleDeath()
    {
        if (isPlayer)
        {
            Debug.Log("Player has died!");
            OnPlayerDeath?.Invoke(); // 플레이어 사망 이벤트 호출
        }
        else
        {
            Debug.Log("Enemy has died!");
            OnEnemyDeath?.Invoke(); // 적 사망 이벤트 호출
            Destroy(gameObject); // 적 오브젝트 파괴
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarController : MonoBehaviour
{
    public Slider healthSlider; // 체력 바 슬라이더
    public TMP_Text healthText; // 체력 텍스트
    private Health health; // 체력 스크립트 참조

    void Start()
    {
        // Health 컴포넌트 찾기
        health = GetComponent<Health>();

        if (health != null)
        {
            // 초기 체력 업데이트
            UpdateHealthBar();

            // 체력 변화 이벤트 연결
            if (health.isPlayer)
            {
                Health.OnPlayerHealthChanged += UpdateHealthBar;
            }
            else
            {
                health.OnEnemyHealthChanged += UpdateHealthBar;
            }
        }
    }

    // 체력 바 업데이트
    private void UpdateHealthBar(int currentHealth = 0)
    {
        if (health == null) return;

        // 체력 슬라이더와 텍스트 갱신
        healthSlider.maxValue = health.baseHealth;
        healthSlider.value = health.currentHealth;
        healthText.text = $"{health.currentHealth}/{health.baseHealth}";
    }

    private void OnDestroy()
    {
        // 이벤트 해제
        if (health.isPlayer)
        {
            Health.OnPlayerHealthChanged -= UpdateHealthBar;
        }
        else
        {
            health.OnEnemyHealthChanged -= UpdateHealthBar;
        }
    }
}

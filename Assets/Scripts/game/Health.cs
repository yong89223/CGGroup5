using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public static event Action OnPlayerDeath; // �÷��̾� ��� �̺�Ʈ
    public event Action<int> OnHealthChanged; // ü�� ��ȭ �̺�Ʈ
    public event Action OnEnemyDeath; // �� ��� �̺�Ʈ

    private void Start()
    {
        currentHealth = maxHealth;
    }

    // ������ ó�� �Լ�
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        OnHealthChanged?.Invoke(currentHealth); // ü�� ��ȭ �̺�Ʈ ȣ��

        if (currentHealth <= 0)
        {
            if (CompareTag("Player"))
            {
                OnPlayerDeath?.Invoke(); // �÷��̾� ��� �̺�Ʈ ȣ��
            }
            else if (CompareTag("Enemy"))
            {
                OnEnemyDeath?.Invoke(); // �� ��� �̺�Ʈ ȣ��
                Destroy(gameObject); // �� ������Ʈ �ı�
            }
        }
    }
}

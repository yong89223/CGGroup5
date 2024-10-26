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
            return;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Bullet hit the enemy."); // Ȯ�ο� �α�

            // ������ ������ ����
            Health enemyHealth = collision.gameObject.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage); // ������ ������ ����
            }

            Destroy(gameObject); // źȯ �ı�
        }
    }
}
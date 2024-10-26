using UnityEngine;
public class EnemyController : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Health>().OnEnemyDeath += HandleEnemyDeath; // �� ��� �̺�Ʈ �߻�
    }

    private void OnDisable()
    {
        GetComponent<Health>().OnEnemyDeath -= HandleEnemyDeath; // �� ��� �̺�Ʈ ����
    }

    private void HandleEnemyDeath()
    {
        // ���⼭ �߰��� ���� ����, ���� ��� ���� �� ����
        Destroy(gameObject); // �� ������Ʈ ����
    }
}
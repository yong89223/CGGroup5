using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public int healthIncrease = 20; // 증가할 체력량

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어 Layer와 충돌 확인
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // 플레이어의 HealthComponent 스크립트 가져오기
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                // 체력 증가 및 아이템 제거
                playerHealth.IncreaseHealth(healthIncrease);
                Destroy(gameObject); // 아이템 제거
            }
        }
    }
}

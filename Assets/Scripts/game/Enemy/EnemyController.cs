using UnityEngine;
public class EnemyController : MonoBehaviour
{
    private void OnEnable()
    {
        var health = GetComponent<Health>();
        if (health != null)
        {
            health.OnEnemyDeath -= HandleEnemyDeath; // 중복 구독 방지
            health.OnEnemyDeath += HandleEnemyDeath; // 이벤트 구독
        }
    }


    private void OnDisable()
    {
        GetComponent<Health>().OnEnemyDeath -= HandleEnemyDeath; // 적 사망 이벤트 해제
    }

    private void HandleEnemyDeath()
    {
        // 여기서 추가로 점수 증가, 사운드 재생 넣을 수 있음
    }
}
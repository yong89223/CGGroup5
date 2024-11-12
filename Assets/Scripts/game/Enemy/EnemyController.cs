using UnityEngine;
public class EnemyController : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Health>().OnEnemyDeath += HandleEnemyDeath; // 적 사망 이벤트 발생
    }

    private void OnDisable()
    {
        GetComponent<Health>().OnEnemyDeath -= HandleEnemyDeath; // 적 사망 이벤트 해제
    }

    private void HandleEnemyDeath()
    {
        // 여기서 추가로 점수 증가, 사운드 재생 넣을 수 있음
        Destroy(gameObject); // 적 오브젝트 제거
    }
}
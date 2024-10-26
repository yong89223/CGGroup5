using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public GameObject respawnPanel; // 리스폰 패널 오브젝트

    private void OnEnable()
    {
        PlayerMovement.OnMoveInput += MovePlayer;
        Health.OnPlayerDeath += ShowRespawnPanel; //사망 이벤트
    }

    private void OnDisable()
    {
        PlayerMovement.OnMoveInput -= MovePlayer;
        Health.OnPlayerDeath -= ShowRespawnPanel; // 사망 이벤트 해제
    }

    private void MovePlayer(Vector3 moveDirection)
    {
        transform.position += moveDirection * Time.deltaTime;
    }

    

    private void ShowRespawnPanel()
    {
        respawnPanel.SetActive(true); // 리스폰 패널 표시
        // 추가로, 플레이어의 움직임이나 공격을 비활성화하는 등의 처리를 할 수 있습니다.
    }
}

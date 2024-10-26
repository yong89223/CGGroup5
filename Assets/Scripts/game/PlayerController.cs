using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public GameObject respawnPanel; // ������ �г� ������Ʈ

    private void OnEnable()
    {
        PlayerMovement.OnMoveInput += MovePlayer;
        Health.OnPlayerDeath += ShowRespawnPanel; //��� �̺�Ʈ
    }

    private void OnDisable()
    {
        PlayerMovement.OnMoveInput -= MovePlayer;
        Health.OnPlayerDeath -= ShowRespawnPanel; // ��� �̺�Ʈ ����
    }

    private void MovePlayer(Vector3 moveDirection)
    {
        transform.position += moveDirection * Time.deltaTime;
    }

    

    private void ShowRespawnPanel()
    {
        respawnPanel.SetActive(true); // ������ �г� ǥ��
        // �߰���, �÷��̾��� �������̳� ������ ��Ȱ��ȭ�ϴ� ���� ó���� �� �� �ֽ��ϴ�.
    }
}

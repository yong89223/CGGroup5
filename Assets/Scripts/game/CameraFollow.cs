using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform ����
    public Vector3 offset; // ī�޶�� �÷��̾� �� �Ÿ�

    void Start()
    {
        // �ʱ� ������ ����
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // �� �����Ӹ��� ī�޶� ��ġ�� ������Ʈ
        transform.position = player.position + offset;
    }
}

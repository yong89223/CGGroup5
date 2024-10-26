using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform 참조
    public Vector3 offset; // 카메라와 플레이어 간 거리

    void Start()
    {
        // 초기 오프셋 설정
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // 매 프레임마다 카메라 위치를 업데이트
        transform.position = player.position + offset;
    }
}

using UnityEngine;

public class ParallaxBackground_Type01 : MonoBehaviour
{
    [SerializeField]
    private Transform target;                // 현재 배경과 이어지는 배경
    [SerializeField]
    private float moveSpeed;                 // 이동 속도
    [SerializeField]
    private Vector3 moveDirection;           // 이동 방향

    private float backgroundWidth;           // 배경의 너비
    private Vector3 initialPosition;         // 초기 위치 저장

    private void Start()
    {
        // 배경이 RectTransform을 사용하는 경우
        RectTransform rectTransform = GetComponent<RectTransform>();
        backgroundWidth = rectTransform.rect.width;  // RectTransform의 너비 사용
        initialPosition = transform.position;         // 초기 위치 저장
    }

    private void Update()
    {
        // 배경이 moveDirection 방향으로 moveSpeed의 속도로 이동
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // 배경이 왼쪽으로 이동하여 화면을 벗어나면
        if (transform.position.x <= -backgroundWidth/2)
        {
            // 배경을 오른쪽 끝으로 이동
            transform.position += new Vector3(backgroundWidth * 2, 0, 0);
        }
    }
}

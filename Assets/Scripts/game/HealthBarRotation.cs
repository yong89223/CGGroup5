using UnityEngine;

public class HealthBarRotation : MonoBehaviour
{
    public Camera mainCamera; // 카메라 참조

    void Start()
    {
        if (mainCamera == null)
        {
            // 메인 카메라 자동 할당
            mainCamera = Camera.main;
        }
    }

    void LateUpdate()
    {
        // HP 바를 카메라 방향으로 고정
        transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
    }
}
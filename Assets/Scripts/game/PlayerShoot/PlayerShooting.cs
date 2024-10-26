using UnityEngine;
using System;

public class PlayerShooting : MonoBehaviour
{
    public static event Action<Vector3> OnShoot; // 탄환 발사 이벤트

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                // 마우스 위치의 Y 값을 0.5로 고정
                Vector3 shootDirection = new Vector3(hitInfo.point.x, 0.5f, hitInfo.point.z) - transform.position;
                shootDirection = shootDirection.normalized;

                OnShoot?.Invoke(shootDirection); // 탄환 발사 이벤트 호출
            }
        }
    }
}

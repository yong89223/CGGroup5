using UnityEngine;
using System;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    public static event Action<Vector3> OnShoot; // 탄환 발사 이벤트
    public bool canShoot = true;
    public float shootCooldown = 3.0f; // 쿨다운 시간
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            canShoot = false;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                Vector3 shootDirection = (hitInfo.point - transform.position).normalized;
                shootDirection.y = 0; // Y축 제거 (수평 방향)

                OnShoot?.Invoke(shootDirection); // 발사 이벤트 호출
            }

            StartCoroutine(ShootingCooldown());
        }
    }

    private IEnumerator ShootingCooldown()
    {
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }
}

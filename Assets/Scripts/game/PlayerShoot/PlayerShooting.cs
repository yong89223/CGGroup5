using UnityEngine;
using System;

public class PlayerShooting : MonoBehaviour
{
    public static event Action<Vector3> OnShoot; // źȯ �߻� �̺�Ʈ

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                // ���콺 ��ġ�� Y ���� 0.5�� ����
                Vector3 shootDirection = new Vector3(hitInfo.point.x, 0.5f, hitInfo.point.z) - transform.position;
                shootDirection = shootDirection.normalized;

                OnShoot?.Invoke(shootDirection); // źȯ �߻� �̺�Ʈ ȣ��
            }
        }
    }
}

using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    public static event Action<Vector3> OnMoveInput; // 이동 입력 이벤트
    public float moveSpeed = 3f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveX* moveSpeed, 0, moveZ * moveSpeed);
        OnMoveInput?.Invoke(moveDirection); // 이동 입력 이벤트 호출
    }
}
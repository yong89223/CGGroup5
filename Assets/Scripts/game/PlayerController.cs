using UnityEngine;
public class PlayerController : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerMovement.OnMoveInput += MovePlayer;
    }

    private void OnDisable()
    {
        PlayerMovement.OnMoveInput -= MovePlayer;
    }

    private void MovePlayer(Vector3 moveDirection)
    {
        transform.position += moveDirection * Time.deltaTime;
    }
}

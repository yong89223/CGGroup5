using UnityEngine;

public class ChestOpenHandler : MonoBehaviour
{
    [SerializeField]
    private Animator chestAnimator; // 상자(Chest)의 Animator

    [SerializeField]
    private string openAnimationName = "OpenAnimation"; // 실행할 애니메이션 이름

    public void PlayOpenAnimation()
    {
        if (chestAnimator != null)
        {
            chestAnimator.Play(openAnimationName);
        }
        else
        {
            Debug.LogWarning("Animator가 연결되지 않았습니다!");
        }
    }
}

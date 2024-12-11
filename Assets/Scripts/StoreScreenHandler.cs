using UnityEngine;

public class StoreScreenHandler : MonoBehaviour
{
    public GameObject GunsMenuUI;
    public GameObject ItemsMenuUI;
    public GameObject SkinsMenuUI;
    public GameObject OpenChestObject;

    private void Start()
    {
        // "StoreScreen"의 클릭 이벤트 등록
        if (TryGetComponent(out UnityEngine.UI.Button button))
        {
            button.onClick.AddListener(CloseAllMenus);
        }
        else
        {
            Debug.LogWarning("Button component not found on the StoreScreen object.");
        }
    }

    public void CloseAllMenus()
    {
        if (GunsMenuUI != null) GunsMenuUI.SetActive(false);
        if (ItemsMenuUI != null) ItemsMenuUI.SetActive(false);
        if (SkinsMenuUI != null) SkinsMenuUI.SetActive(false);
        
        // OpenChestObject의 자식의 자식만 비활성화
        if (OpenChestObject != null)
        {
            DeactivateOnlyGrandchildren(OpenChestObject.transform);
        }
    }

    // 자식의 자식만 비활성화하는 함수
    private void DeactivateOnlyGrandchildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            // 자식의 자식들만 비활성화
            foreach (Transform grandchild in child)
            {
                if (grandchild.gameObject.activeSelf) // 활성화된 경우만 체크
                {
                    grandchild.gameObject.SetActive(false);
                }
            }
        }
    }
}

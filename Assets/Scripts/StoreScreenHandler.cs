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
    }

    public void CloseAllMenus()
    {
        if (GunsMenuUI != null) GunsMenuUI.SetActive(false);
        if (ItemsMenuUI != null) ItemsMenuUI.SetActive(false);
        if (SkinsMenuUI != null) SkinsMenuUI.SetActive(false);
        
        // OpenChestObject의 활성화된 자식만 비활성화
        if (OpenChestObject != null)
        {
            foreach (Transform child in OpenChestObject.transform)
            {
                if (child.gameObject.activeSelf) // 활성화된 자식만 체크
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }


}

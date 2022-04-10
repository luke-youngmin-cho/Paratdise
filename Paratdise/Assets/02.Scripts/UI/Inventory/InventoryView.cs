using UnityEngine;


/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/04
/// 최종수정일 : 
/// 설명 : 
/// 
/// 인벤토리 UI
/// </summary>

public class InventoryView : MonoBehaviour
{
    public static InventoryView instance;
    [SerializeField] private GameObject itemInfoPanel;

    //============================================================================
    //************************* Public Methods ***********************************
    //============================================================================

    public void ActiveInfoPanel(Sprite icon, string name, string discription)
    {
        itemInfoPanel.GetComponent<InventoryItemInfoPanel>().Setup(icon, name, discription);
        itemInfoPanel.SetActive(true);
    }


    //============================================================================
    //************************* Private Methods **********************************
    //============================================================================

    private void Awake()
    {
        instance = this;
    }
}
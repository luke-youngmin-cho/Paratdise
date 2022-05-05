using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private Image bg;
    [SerializeField] private Sprite bg_mice;
    [SerializeField] private Sprite bg_laila;
    [SerializeField] private Sprite bg_drillggabijo;
    [SerializeField] private Sprite bg_eily;
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

    private void OnEnable()
    {
        switch (GameManager.characterSelected)
        {
            case CharacterType.None:
                break;
            case CharacterType.Mice:
                bg.sprite = bg_mice;
                break;
            case CharacterType.Laila:
                bg.sprite = bg_laila;
                break;
            case CharacterType.DrillGgabijo:
                bg.sprite = bg_drillggabijo;
                break;
            case CharacterType.Ailey:
                bg.sprite = bg_eily;
                break;
            default:
                break;
        }
    }
}
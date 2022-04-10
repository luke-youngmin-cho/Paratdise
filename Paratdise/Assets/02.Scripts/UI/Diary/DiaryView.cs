using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/04
/// 최종수정일 : 
/// 설명 : 
/// 
/// 다이어리 UI
/// </summary>
/// 
public class DiaryView : MonoBehaviour
{
    public static DiaryView instance;
    [SerializeField] private GameObject capsuleInfoPanel;

    //============================================================================
    //************************* Public Methods ***********************************
    //============================================================================

    public void ActiveInfoPanel(Sprite icon, string title, string discription)
    {
        capsuleInfoPanel.GetComponent<DiaryCapsuleInfoPanel>().Setup(icon, title, discription);
        capsuleInfoPanel.SetActive(true);
    }


    //============================================================================
    //************************* Private Methods **********************************
    //============================================================================

    private void Awake()
    {
        instance = this;
    }
}

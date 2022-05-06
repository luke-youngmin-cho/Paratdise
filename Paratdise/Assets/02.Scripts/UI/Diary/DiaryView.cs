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
    [SerializeField] private GameObject pieceOfStoryInfoPanel;
    [SerializeField] private GameObject endingCardInfoPanel;
    [SerializeField] private GameObject tutorial;
    //============================================================================
    //************************* Public Methods ***********************************
    //============================================================================

    public void ActivePiecesOfStoryInfoPanel(Sprite icon, string title, string discription)
    {
        pieceOfStoryInfoPanel.GetComponent<PieceOfStoryInfoPanel>().Setup(icon, title, discription);
        pieceOfStoryInfoPanel.SetActive(true);
    }

    public void ActiveEndingCardInfoPanel(Sprite icon, string title, string indexText)
    {
        endingCardInfoPanel.GetComponent<EndingCardInfoPanel>().Setup(icon, title, indexText);
        endingCardInfoPanel.SetActive(true);
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
        if (Settings.instance.Tutorial_Diary == 0)
        {
            tutorial.SetActive(true);
            Settings.instance.Tutorial_Diary = 1;
        }            
    }
}

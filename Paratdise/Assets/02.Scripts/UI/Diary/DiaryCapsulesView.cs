using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/09
/// 최종수정일 : 
/// 설명 : 
/// 
/// 다이어리 UI
/// </summary>

public class DiaryCapsulesView : MonoBehaviour
{
    public static DiaryCapsulesView instance;
    public TimeCapsuleRarity type;
    public Transform itemContent;
    public GameObject slotPrefab;
    public List<GameObject> slots = new List<GameObject>();
    [SerializeField] private GameObject diaryInfoPanel;

    //============================================================================
    //************************* Public Methods ***********************************
    //============================================================================

    public void RefreshItemList()
    {
        if (PlayerDataManager.data == null) return;

        for (int i = slots.Count - 1; i > -1; i--)
            Destroy(slots[i]);
        slots.Clear();

        foreach (var item in PlayerDataManager.data.timeCapsulesData)
        {
            TimeCapsule timeCapsule = TimeCapsuleAssets.GetTimeCapsule(item.index);
            if ((timeCapsule.type == type) &&
                (item.num > 0))
            {
                GameObject slot = Instantiate(slotPrefab, itemContent);
                slots.Add(slot);
                
                slot.GetComponent<DiarySlot>().SetInfo(timeCapsule.icon, timeCapsule.title, timeCapsule.num, timeCapsule.discription);
            }
        }
    }

    public void ActiveInfoPanel(Sprite icon, string title, string discription)
    {
        diaryInfoPanel.GetComponent<DiaryCapsuleInfoPanel>().Setup(icon, title, discription);
        diaryInfoPanel.SetActive(true);
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
        RefreshItemList();
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/05
/// 최종수정일 : 
/// 설명 : 
/// 
/// 스테이지 클리어시 띄우는 창
/// </summary>
public class StageClearPopUp : MonoBehaviour
{
    [SerializeField] private Text clearTimeText;
    [SerializeField] private Transform earnedResourcesContent;
    [SerializeField] private GameObject slotOrigin;

    private List<GameObject> slots = new List<GameObject>();
    public void Refresh()
    {
        clearTimeText.text = StageManager.instance.GetElapsedTime().ToString();

        for (int i = slots.Count - 1; i > -1; --i)
        {
            Destroy(slots[i]);
        }

        foreach (var itemInfo in StageManager.instance.GetEarnedItems())
        {
            GameObject go = Instantiate(slotOrigin, earnedResourcesContent);
            go.GetComponent<Image>().sprite = ItemAssets.instance.GetItemByName(itemInfo.itemName).icon;
            go.transform.GetChild(0).GetComponent<Text>().text = itemInfo.num.ToString();
            go.SetActive(true);
            slots.Add(go);
        }
        slotOrigin.SetActive(false);
    }


    private void OnEnable()
    {
        Refresh();
    }

    private void Update()
    {
        if (PlayStateManager.instance.currentPlayState != PlayState.Paused)
            PlayStateManager.instance.SetState(PlayState.Paused);
    }
}

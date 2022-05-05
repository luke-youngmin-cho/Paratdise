using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/05/05
/// ���������� : 
/// ���� : 
/// 
/// �������� Ŭ����� ���� â
/// </summary>
public class StageClearPopUp : MonoBehaviour
{
    [SerializeField] private Text clearTimeText;
    [SerializeField] private Transform earnedResourcesContent;
    [SerializeField] private GameObject slotOrigin;

    private void OnEnable()
    {
        clearTimeText.text =  StageManager.instance.GetElapsedTime().ToString();
        foreach (var itemInfo in StageManager.instance.GetEarnedItems())
        {
            GameObject go = Instantiate(slotOrigin, earnedResourcesContent);
            go.GetComponent<Image>().sprite = ItemAssets.instance.GetItemByName(itemInfo.itemName).icon;
            go.transform.GetChild(0).GetComponent<Text>().text = itemInfo.num.ToString();
            go.SetActive(true);
        }
        slotOrigin.SetActive(false);
    }
}

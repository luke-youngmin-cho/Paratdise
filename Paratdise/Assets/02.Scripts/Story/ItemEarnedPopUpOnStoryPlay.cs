using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/26
/// 최종수정일 : 
/// 설명 : 
/// 
/// 연출완료후 아이템획득해야할때 팝업시킬 패널
/// </summary>
public class ItemEarnedPopUpOnStoryPlay : MonoBehaviour
{
    [SerializeField] private Item itemToEarn;

    private void OnEnable()
    {
        transform.Find("Icon").GetComponent<Image>().sprite = itemToEarn.icon;
        transform.Find("Name").GetComponent<Text>().text = itemToEarn.itemName;
        transform.Find("Description").GetComponent<Text>().text = itemToEarn.description;
        EarnItem();
    }

    private void EarnItem()
    {
        ItemData itemData = new ItemData()
        {
            itemName = itemToEarn.itemName,
            num = itemToEarn.num
        };
        InventoryDataManager.data.AddData(itemData);
    }
}
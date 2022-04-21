using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public int targetToolLevel;//데이터 저장할 임시변수(1~10단계)
    public UpgradeToolRegion targetToolRegion;//enum 길이 파워 등

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //전역변수로 업그레이드 시
    public void ToolUpgrader()
    {
        UpgradeCheck(GetCharacterType(ChooseCharacterUI.currentCharacterIndex), targetToolRegion, targetToolLevel);
        //실행시upgrade
        //캐릭터 받고, 어떤 툴인지, 몇단계 툴인지
    }

    private void UpgradeCheck(CharacterType _character, UpgradeToolRegion _region, int _level)
    {
        List<Ingredients>[] targetRegion = GetUpgradeToolType(_region);//region으로 툴 타입 알려줌.
        List<ItemData> tempItemDatas = new List<ItemData>();//필요한 아이템들을 리스트로 임시 저장.
        for (int ingredientType = 0; ingredientType < targetRegion.Length; ingredientType++)
        {
            int toolLevel = GetUpgradeLevel(_level);//몇단계 강화인지 파악.
            ItemData targetItem = InventoryDataManager.data.itemsData.Find(x => x.itemName == targetRegion[toolLevel][ingredientType].name);
            //itemData 가 아이템들의 정보를 갖고 있다면, 해당 아이템의 정보를 InventoryDataManager.data.itemsData에서 찾는다.
            if (!InventoryDataManager.instance.dataDictionary[_character].itemsData.Contains(targetItem))//캐릭터의 enum을 넣으면 해당 인벤토리 반환 -> 아이템 체크
                return; //만약 타겟아이템이 인벤토리에 없다면 함수 종료.

            tempItemDatas.Add(targetItem);//만약 있다면 필요한 아이템 임시 리스트에 저장.
        }

        foreach (var item in tempItemDatas)//강화에 필요한 아이템들을 하나씩 소모.
        {
            InventoryDataManager.instance.dataDictionary[_character].RemoveData(item);//removeData로 아이템 소모.
        }

        GameInfoController.toolInfos[ChooseCharacterUI.currentCharacterIndex].reinforceCount += 1; // 아이템 강화 횟수를 1 증가.

        if (targetToolRegion == UpgradeToolRegion.Length)
            GameInfoController.toolInfos[ChooseCharacterUI.currentCharacterIndex].toolHeight += 0.05f;
        else if (targetToolRegion == UpgradeToolRegion.Width)
            GameInfoController.toolInfos[ChooseCharacterUI.currentCharacterIndex].toolWidth += 0.05f;
        else if (targetToolRegion == UpgradeToolRegion.Luck)
            GameInfoController.toolInfos[ChooseCharacterUI.currentCharacterIndex].luck += 5f;
        else if (targetToolRegion == UpgradeToolRegion.Strength)
            GameInfoController.toolInfos[ChooseCharacterUI.currentCharacterIndex].power += 0.25f;
        //실질적인 해당 부위 강화.
    }

    private int GetUpgradeLevel(int targetLevel)
    {
        int _upgradeLevel = -1;
        if (targetLevel >= 0 && targetLevel < 4)
            _upgradeLevel = 0;
        else if (targetLevel >= 4 && targetLevel < 7)
            _upgradeLevel = 1;
        else if (targetLevel >= 7 && targetLevel < 9)
            _upgradeLevel = 2;
        else if (targetLevel >= 9 && targetLevel < 11)
            _upgradeLevel = 3;

        return _upgradeLevel;
    }

    private List<Ingredients>[] GetUpgradeToolType(UpgradeToolRegion targetRegion)
        //enum type의 upgradetoolregion 받으면 해당 부위의 upgradeinfo 클래스의 ingredients 리스트 반환. 
    {
        List<Ingredients>[] _region = null;
        if (targetRegion == UpgradeToolRegion.Length)
            _region = GameInfoController.upgradeInfo.length;
        else if (targetRegion == UpgradeToolRegion.Width)
            _region = GameInfoController.upgradeInfo.width;
        else if (targetRegion == UpgradeToolRegion.Luck)
            _region = GameInfoController.upgradeInfo.luck;
        else if (targetRegion == UpgradeToolRegion.Strength)
            _region = GameInfoController.upgradeInfo.strength;

        return _region;
    }

    #region ChracterTypeFind
    private CharacterType GetCharacterType(int byCount)
    {
        CharacterType _type = CharacterType.None;
        switch (byCount)
        {
            case 1:
                _type = CharacterType.Mise;
                break;
            case 2:
                _type = CharacterType.Laila;
                break;
            case 3:
                _type = CharacterType.DrillGgabijo;
                break;
            case 4:
                _type = CharacterType.Eily;
                break;
        }
        return _type;
    }

    private CharacterType GetCharacterType(string byName)
    {
        CharacterType _type = CharacterType.None;
        switch (byName)
        {
            case "마이스":
                _type = CharacterType.Mise;
                break;
            case "라일라":
                _type = CharacterType.Laila;
                break;
            case "깨비드릴조":
                _type = CharacterType.DrillGgabijo;
                break;
            case "에일리":
                _type = CharacterType.Eily;
                break;
        }
        return _type;
    }
    #endregion

    public enum UpgradeToolRegion
    {
        Length,
        Width,
        Luck,
        Strength,
    }
}

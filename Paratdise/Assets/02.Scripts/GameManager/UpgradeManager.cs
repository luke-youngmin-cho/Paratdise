using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;
    public UpgradeToolRegion targetToolRegion;//enum 길이 파워 등

    private CharacterType characterType;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //전역변수로 업그레이드 시
    public void ToolUpgrader(int _targetRegion)
    {
        GetCharacterTypeByIndex();
        UpgradeCheck(characterType, (UpgradeToolRegion)_targetRegion);
    }

    private void GetCharacterTypeByIndex()
    {
        switch (ChooseCharacterUI.currentCharacterIndex)
        {
            case 0:
                characterType = CharacterType.Mice;
                break;
            case 1:
                characterType = CharacterType.Laila;
                break;
            case 2:
                characterType = CharacterType.DrillGgabijo;
                break;
            case 3:
                characterType = CharacterType.Eily;
                break;
        }
    }

    private void UpgradeCheck(CharacterType _character, UpgradeToolRegion _region)
    {
        CharacterData characterData = PlayerDataManager.data.GetCharacterData(_character);

        List<Ingredients>[] targetRegion = GetUpgradeToolType(_region);//region으로 툴 타입 알려줌.
        List<ItemData> tempItemDatas = new List<ItemData>();//필요한 아이템들을 리스트로 임시 저장.
        for (int ingredientType = 0; ingredientType < targetRegion.Length; ingredientType++)
        {
            int toolLevel = GetUpgradeLevel(characterData, _region);//몇단계 강화인지 파악.
            ItemData targetItem = InventoryDataManager.data.itemsData.Find(x => x.itemName == targetRegion[toolLevel][ingredientType].name);
            //itemData 가 아이템들의 정보를 갖고 있다면, 해당 아이템의 정보를 InventoryDataManager.data.itemsData에서 찾는다.
            
            if (!InventoryDataManager.data.itemsData.Contains(targetItem))
            {
                if (InventoryDataManager.data.itemsData.Find(x => x.itemName == targetItem.itemName).num < targetRegion[toolLevel][ingredientType].count)
                    return; //만약 타겟아이템이 인벤토리에 없다면 함수 종료.
            }//캐릭터의 enum을 넣으면 해당 인벤토리 반환 -> 아이템 체크

            tempItemDatas.Add(targetItem);//만약 있다면 필요한 아이템 임시 리스트에 저장.
        }

        foreach (var item in tempItemDatas)//강화에 필요한 아이템들을 하나씩 소모.
        {
            InventoryDataManager.data.RemoveData(item);//removeData로 아이템 소모.
        }

        ToolLevelUp(_region, characterData);
        SetCharacterIdentitiy(_character, characterData);
        //GameInfoController.toolInfos[ChooseCharacterUI.currentCharacterIndex].reinforceCount += 1; // 아이템 강화 횟수를 1 증가.
    }

    private void SetCharacterIdentitiy(CharacterType type, CharacterData data)
    {
        ToolInfo target = GameInfoController.GetToolByCharacter(type, GameInfoController.usingTools);
        target.toolHeight = GameInfoController.GetToolByCharacter(type).toolHeight + 0.05f * data.toolsLevel.heightLevel;
        target.toolWidth = GameInfoController.GetToolByCharacter(type).toolWidth + 0.05f * data.toolsLevel.widthLevel;
        target.luck = GameInfoController.GetToolByCharacter(type).luck + 5 * data.toolsLevel.luckLevel;
        target.power = (float)(GameInfoController.GetToolByCharacter(type).power + 0.25 * data.toolsLevel.strengthLevel);
    }

    private int GetUpgradeLevel(CharacterData data, UpgradeToolRegion targetRegion)
    {
        int _upgradeLevel = -1;
        int targetLevel = 0;

        if (targetRegion == UpgradeToolRegion.Length)
            targetLevel = data.toolsLevel.heightLevel;
        else if (targetRegion == UpgradeToolRegion.Width)
            targetLevel = data.toolsLevel.widthLevel;
        else if (targetRegion == UpgradeToolRegion.Luck)
            targetLevel = data.toolsLevel.luckLevel;
        else if (targetRegion == UpgradeToolRegion.Strength)
            targetLevel = data.toolsLevel.strengthLevel;

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

    private void ToolLevelUp(UpgradeToolRegion targetRegion, CharacterData data)
    {
        ToolsLevel temp = data.toolsLevel;
        if (targetRegion == UpgradeToolRegion.Length)
            temp.heightLevel += 1;
        else if (targetRegion == UpgradeToolRegion.Width)
            temp.widthLevel += 1;
        else if (targetRegion == UpgradeToolRegion.Luck)
            temp.luckLevel += 1;
        else if (targetRegion == UpgradeToolRegion.Strength)
            temp.strengthLevel += 1;
        data.toolsLevel = temp;
    }

    private List<Ingredients>[] GetUpgradeToolType(UpgradeToolRegion targetRegion)
    {
        //enum type의 upgradetoolregion 받으면 해당 부위의 upgradeinfo 클래스의 ingredients 리스트 반환. 
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
                _type = CharacterType.Mice;
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
                _type = CharacterType.Mice;
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

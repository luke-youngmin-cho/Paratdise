using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;
    public Button[] buttons;
    public Image targetToolImage;
    public Sprite[] toolImages;
    public UpgradeToolRegion targetToolRegion;

    private CharacterType characterType;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {

    }


    private void OnEnable()
    {
        ViewTargetToolImage();
    }

    void Update()
    {

    }


    public void ToolUpgradeNow(int _targetRegion)
    {
        // 0-->Length,
        // 1-->Width,
        // 2-->Luck,
        // 3-->Strength,
        characterType = GameInfoController.GetCharacterTypeByIndex(ChooseCharacterUI.currentCharacterIndex);
        GetCharacterTypeByIndex();
        UpgradeCheck(characterType, (UpgradeToolRegion)_targetRegion);
    }

    private void ViewTargetToolImage()
    {
        targetToolImage.sprite = toolImages[ChooseCharacterUI.currentCharacterIndex];
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
        List<Ingredients>[] targetRegion = GetUpgradeToolType(_region);
        List<ItemData> tempItemDatas = new List<ItemData>();


        InventoryData data = InventoryDataManager.data;


        for (int ingredientType = 0; ingredientType < targetRegion.Length; ingredientType++)
        {
            int toolLevel = GetUpgradeLevel(_region);
            ItemData targetItem = data.itemsData.Find(x => x.itemName == targetRegion[toolLevel][ingredientType].name);

            if (!data.itemsData.Contains(targetItem))
            {
                if (data.itemsData.Find(x => x.itemName == targetItem.itemName).num < targetRegion[toolLevel][ingredientType].count)
                    return;
            }

            tempItemDatas.Add(targetItem);
        }

        foreach (var item in tempItemDatas)
        {
            data.RemoveData(item);
        }


        InventoryDataManager.data = data;

        // ToolLevelUp 
        ToolLevelUp(_region);

        CharacterType type = GameInfoController.GetCharacterTypeByIndex(ChooseCharacterUI.currentCharacterIndex);
        PlayerData tmpPlayerData = PlayerDataManager.data;
        CharacterData characterData = tmpPlayerData.GetCharacterData(type);

        SetCharacterIdentitiy(_character, characterData);
        //GameInfoController.toolInfos[ChooseCharacterUI.currentCharacterIndex].reinforceCount += 1;
    }

    private void SetCharacterIdentitiy(CharacterType type, CharacterData data)
    {
        ToolInfo target = GameInfoController.GetToolByCharacter(type, GameInfoController.usingTools);
        target.toolHeight = GameInfoController.GetToolByCharacter(type).toolHeight + 0.05f * data.toolsLevel.heightLevel;
        target.toolWidth = GameInfoController.GetToolByCharacter(type).toolWidth + 0.05f * data.toolsLevel.widthLevel;
        target.luck = GameInfoController.GetToolByCharacter(type).luck + 5 * data.toolsLevel.luckLevel;
        target.power = (float)(GameInfoController.GetToolByCharacter(type).power + 0.25 * data.toolsLevel.strengthLevel);
    }

    private int GetUpgradeLevel(UpgradeToolRegion targetRegion)
    {
        CharacterType type = GameInfoController.GetCharacterTypeByIndex(ChooseCharacterUI.currentCharacterIndex);
        PlayerData tmpPlayerData = PlayerDataManager.data;
        CharacterData data = tmpPlayerData.GetCharacterData(type);

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

    private void ToolLevelUp(UpgradeToolRegion targetRegion)
    {
        CharacterType type = GameInfoController.GetCharacterTypeByIndex(ChooseCharacterUI.currentCharacterIndex);
        PlayerData tmpPlayerData = PlayerDataManager.data;
        CharacterData characterData = tmpPlayerData.GetCharacterData(type);

        ToolsLevel temp = characterData.toolsLevel;
        if (targetRegion == UpgradeToolRegion.Length)
            temp.heightLevel += 1;
        else if (targetRegion == UpgradeToolRegion.Width)
            temp.widthLevel += 1;
        else if (targetRegion == UpgradeToolRegion.Luck)
            temp.luckLevel += 1;
        else if (targetRegion == UpgradeToolRegion.Strength)
            temp.strengthLevel += 1;
        characterData.toolsLevel = temp;

        PlayerDataManager.data = tmpPlayerData;
    }

    private List<Ingredients>[] GetUpgradeToolType(UpgradeToolRegion targetRegion)
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
            case "애일리":
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
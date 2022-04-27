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
    public UpgradeToolRegion targetToolRegion;//enum Í∏∏Ïù¥ ?åå?õå ?ì±

    private CharacterType characterType;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        
    }

    //æ¿ Ω««‡Ω√ø° ¥ÎªÛ µµ±∏ ¿ÃπÃ¡ˆ ∫∏ø©¡‹
    private void OnEnable()
    {
        ViewTargetToolImage();
    }

    void Update()
    {
        
    }

    //?†Ñ?ó≠Î≥??àòÎ°? ?óÖÍ∑∏Î†à?ù¥?ìú ?ãú
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
        CharacterData characterData = PlayerDataManager.data.GetCharacterData(_character);

        List<Ingredients>[] targetRegion = GetUpgradeToolType(_region);//region?úºÎ°? ?à¥ ????ûÖ ?ïå?†§Ï§?.
        List<ItemData> tempItemDatas = new List<ItemData>();//?ïÑ?öî?ïú ?ïÑ?ù¥?Öú?ì§?ùÑ Î¶¨Ïä§?ä∏Î°? ?ûÑ?ãú ????û•.
        for (int ingredientType = 0; ingredientType < targetRegion.Length; ingredientType++)
        {
            int toolLevel = GetUpgradeLevel(characterData, _region);//Î™áÎã®Í≥? Í∞ïÌôî?ù∏Ïß? ?åå?ïÖ.
            ItemData targetItem = InventoryDataManager.data.itemsData.Find(x => x.itemName == targetRegion[toolLevel][ingredientType].name);
            //itemData Í∞? ?ïÑ?ù¥?Öú?ì§?ùò ?†ïÎ≥¥Î?? Í∞ñÍ≥† ?ûà?ã§Î©?, ?ï¥?ãπ ?ïÑ?ù¥?Öú?ùò ?†ïÎ≥¥Î?? InventoryDataManager.data.itemsData?óê?Ñú Ï∞æÎäî?ã§.
            
            if (!InventoryDataManager.data.itemsData.Contains(targetItem))
            {
                if (InventoryDataManager.data.itemsData.Find(x => x.itemName == targetItem.itemName).num < targetRegion[toolLevel][ingredientType].count)
                    return; //ÎßåÏïΩ ???Í≤üÏïÑ?ù¥?Öú?ù¥ ?ù∏Î≤§ÌÜ†Î¶¨Ïóê ?óÜ?ã§Î©? ?ï®?àò Ï¢ÖÎ£å.
            }//Ï∫êÎ¶≠?Ñ∞?ùò enum?ùÑ ?Ñ£?úºÎ©? ?ï¥?ãπ ?ù∏Î≤§ÌÜ†Î¶? Î∞òÌôò -> ?ïÑ?ù¥?Öú Ï≤¥ÌÅ¨

            tempItemDatas.Add(targetItem);//ÎßåÏïΩ ?ûà?ã§Î©? ?ïÑ?öî?ïú ?ïÑ?ù¥?Öú ?ûÑ?ãú Î¶¨Ïä§?ä∏?óê ????û•.
        }

        foreach (var item in tempItemDatas)//Í∞ïÌôî?óê ?ïÑ?öî?ïú ?ïÑ?ù¥?Öú?ì§?ùÑ ?ïò?Çò?î© ?ÜåÎ™?.
        {
            InventoryDataManager.data.RemoveData(item);//removeDataÎ°? ?ïÑ?ù¥?Öú ?ÜåÎ™?.
        }

        ToolLevelUp(_region, characterData);
        SetCharacterIdentitiy(_character, characterData);
        //GameInfoController.toolInfos[ChooseCharacterUI.currentCharacterIndex].reinforceCount += 1; // ?ïÑ?ù¥?Öú Í∞ïÌôî ?öü?àòÎ•? 1 Ï¶ùÍ??.
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
        //enum type?ùò upgradetoolregion Î∞õÏúºÎ©? ?ï¥?ãπ Î∂??úÑ?ùò upgradeinfo ?Å¥?ûò?ä§?ùò ingredients Î¶¨Ïä§?ä∏ Î∞òÌôò. 
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
            case "ÎßàÏù¥?ä§":
                _type = CharacterType.Mice;
                break;
            case "?ùº?ùº?ùº":
                _type = CharacterType.Laila;
                break;
            case "Íπ®ÎπÑ?ìúÎ¶¥Ï°∞":
                _type = CharacterType.DrillGgabijo;
                break;
            case "?óê?ùºÎ¶?":
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ۼ��� : �Ǽ���
/// �����ۼ��� : 2022/04/13
/// ���������� : 
/// ���� : 
/// 
/// ĳ���� ���� UI
/// </summary>
public class ChooseCharacterUI : MonoBehaviour
{
    public static int currentCharacterIndex = 0;
    public Text characterNameText;
    public Text coldResistanceText;
    public Text sanityText;

    [Header("___TOOL INFORMATION___")]
    public Text toolNameText;
    public Text toolWidth;
    public Text toolHeight;
    public Text toolLuckText;
    public Text toolPowerText;

    [Header("___CHARACTERS___")]
    public CharacterImage[] characterImages;    

    [Header("___OTHERS___")]
    public Text talkText;
    public Text hpText;
    public Image charImg;

    void Start()
    {
        ViewCharacter(currentCharacterIndex);
    }

    public void CharImgChange(CharacterType characData)
    {
        CharacterData data = PlayerDataManager.data.GetCharacterData(characData);
        OpenSelectedCharacter(currentCharacterIndex, data.isAvailable);
    }

    public void OpenUI(GameObject target)
    {
        target.SetActive(true);
    }

    public void CloseUI(GameObject target)
    {
        target.SetActive(false);
    }

    public void CharacterReset()
    {
        CharacterType selectedType = CharacterInfoTable.characterInfos[currentCharacterIndex].characterType;
        PlayerDataManager.data.ResetCharacter(selectedType);
    }

    public void CharInfoChange(int _characterIndex)
    {
        CharacterInfo characterInfo = CharacterInfoTable.characterInfos[_characterIndex];
        CharImgChange(characterInfo.characterType);
        //charImg.sprite = Resources.Load<Sprite>("CharacterImage/character" + _characterIndex);
        characterNameText.text = characterInfo.characterName;
        coldResistanceText.text = characterInfo.coldResistance+" ";
        sanityText.text = characterInfo.sanity + "";
        talkText.text = CharacterInfoTable.characterScripts[_characterIndex, Random.Range(0, 3)];
    }

    public void ToolInfoChange(ToolType _type)
    {
        ToolInfo toolInfo = CharacterInfoTable.toolInfos.Find(x => x.toolType == _type);
        toolNameText.text = toolInfo.toolName;
        toolHeight.text = toolInfo.toolHeight+"";
        toolWidth.text = toolInfo.toolWidth + "";
        toolLuckText.text = toolInfo.luck + "";
        toolPowerText.text = toolInfo.power + "";
    }

    public void ShowNext()
    {
        if (currentCharacterIndex == CharacterInfoTable.characterInfos.Count - 1)
            return;
        currentCharacterIndex += 1;
        ViewCharacter(currentCharacterIndex);
    }

    public void ShowPrev()
    {
        if (currentCharacterIndex == 0)
            return;
        currentCharacterIndex -= 1;
        ViewCharacter(currentCharacterIndex);
    }

    private void ViewCharacter(int idx)
    {
        CharInfoChange(idx);

        CharacterInfo nowSelectedCharacter = CharacterInfoTable.characterInfos[idx];
        ToolInfoChange(nowSelectedCharacter.toolType);
        //CreateHeartImage((int)nowSelectedCharacter.heart);
        float additionalHp = GameInfoController.GetAdditionalHpByCharacterType(nowSelectedCharacter.characterType);
        hpText.text = nowSelectedCharacter.heart + " (+" + additionalHp+ ")";
        // ������ - 04.12 �߰� :
        GameManager.characterSelected = (CharacterType)(idx + 1);
    }

    private void OpenSelectedCharacter(int _index, bool isAvailable)
    {
        for (int i = 0; i < characterImages.Length; i++)
        {
            characterImages[i].available.SetActive(false);
            characterImages[i].unavailable.SetActive(false);
        }
        characterImages[_index].available.SetActive(isAvailable);
        characterImages[_index].unavailable.SetActive(!isAvailable);
    }

    public void goStart()
    {
        currentCharacterIndex = 0;
    }

    public void LoadpgradeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("RealReinforceTools");
    }
}

[System.Serializable]
public class CharacterImage
{
    public GameObject available;
    public GameObject unavailable;
}

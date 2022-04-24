using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 권세인
/// 최초작성일 : 2022/04/13
/// 최종수정일 : 
/// 설명 : 
/// 
/// 캐릭터 선택 UI
/// </summary>
public class ChooseCharacterUI : MonoBehaviour
{
    public static int currentCharacterIndex = 0;
    public int currentToolIndex = 0;
    public Text characterNameText;
    public Text coldResistanceText;
    public Text sanityText;
    public Text toolNameText;
    public Text toolWidth;
    public Text toolHeight;
    public Text toolLuckText;
    public Text toolPowerText;
    public Text talkText;
    public Image charImg;

    [Header("Available")]
    public Sprite Mise;
    public Sprite Laila;
    public Sprite ggabirilldjo;
    public Sprite Eily;

    [Header("Not Available")]
    public Sprite unLaila;
    public Sprite unGgabirilldjo;
    public Sprite unEily;

    [Header("LIFE")]
    public Transform heartStorage;
    public GameObject heartImage;
    
    public CharacterData characData ;


    // Start is called before the first frame update
    void Start()
    {
        ViewCharacter(currentCharacterIndex);
    }
    public void CharImgChange(CharacterType characData)
    {
        //CharacterData data = PlayerDataManager.data.charactersData.Find(x => x.type == characData);
        CharacterData data = new CharacterData();
        data.isAvailable = false;
        switch(characData)
        {
            case CharacterType.Mice:
                charImg.sprite = Mise;
                break;
            case CharacterType.Laila:
                if (data.isAvailable)
                    charImg.sprite = Laila;
                else
                    charImg.sprite = unLaila;
                break;
            case CharacterType.DrillGgabijo:
                if (data.isAvailable)
                    charImg.sprite = ggabirilldjo;
                else
                    charImg.sprite = unGgabirilldjo;
                break;

            case CharacterType.Eily:
                if (data.isAvailable)
                    charImg.sprite = Eily;
                else
                    charImg.sprite = unEily;
                break;
        }
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
        currentCharacterIndex = 0;
        ViewCharacter(0);
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

    private void CreateHeartImage(int count)
    {
        for (int i = 0; i < heartStorage.childCount; i++)
        {
            Destroy(heartStorage.GetChild(i).gameObject);
        }
        for (int i = 0; i < count; i++)
        {
            GameObject go = Instantiate(heartImage, heartStorage);
            go.SetActive(true);
        }
    }

    private void ViewCharacter(int idx)
    {
        CharInfoChange(idx);

        CharacterInfo nowSelectedCharacter = CharacterInfoTable.characterInfos[idx];
        ToolInfoChange(nowSelectedCharacter.toolType);
        CreateHeartImage((int)nowSelectedCharacter.heart);

        // 조영민 - 04.12 추가 :
        GameManager.characterSelected = (CharacterType)(idx + 1);
    }

    public void goStart()
    {
        currentCharacterIndex = 0;
    }

    public void ShowBefore()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

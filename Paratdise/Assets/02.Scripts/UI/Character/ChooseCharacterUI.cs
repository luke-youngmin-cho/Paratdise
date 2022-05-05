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
    public int currentCharacterIndex = 0;
    public int currentToolIndex = 0;
    public Text characterNameText;
    public Text toolNameText;
    public Text toolWidth;
    public Text toolHeight;
    public Text toolLuckText;
    public Text toolPowerText;
    public Text talkText;
    public Image charImg;
    public Sprite Mise,Laila, ggabirilldjo, Eily;

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
        switch(characData)
        {
            case CharacterType.Mice:
            charImg.sprite= Mise;
            break;

            case CharacterType.Laila:
                charImg.sprite = Laila;
                break;

            case CharacterType.DrillGgabijo:
                charImg.sprite = ggabirilldjo;
                break;

            case CharacterType.Ailey:
                charImg.sprite = Eily;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 권세인
/// 최초작성일 : 2022/04/13
/// 최종수정일 : 
/// 설명 : 
/// 
/// 캐릭터 정보 클래스
/// </summary>
public class CharacterInfo
{
    public int characterIndex;
    public string characterName;
    public float characterHeight;
    public float characterWidth;
    public float heart;
    public float bonusHeart;
    public float movementSpeed;
    public float slightRange;
    public int tools;
    public string gettinCondition;
    public ToolType toolType;
    public CharacterType characterType;

    public CharacterInfo(int _characterIndex, string _characterName, float _characterHeight, 
        float _characterWidth, float _heart, float _bonusHeart, float _movementSpeed, float _slightRange, int _tools, string _gettinCondition,
        ToolType _toolType, CharacterType _characterType)
    {
        characterIndex = _characterIndex;
        characterName=_characterName;
        characterHeight = _characterHeight;
        characterWidth = _characterWidth;
        heart = _heart;
        bonusHeart = _bonusHeart;
        movementSpeed = _movementSpeed;
        slightRange = _slightRange;
        tools = _tools;
        gettinCondition = _gettinCondition;
        toolType = _toolType;
        characterType = _characterType;
    }
}

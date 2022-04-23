using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/28
/// 최종수정일 : 2022/04/23
/// 설명 : 
/// 
/// 캐릭터의 타입과 해금 여부 데이터 
/// 선택지이력, 스토리조각 데이터 추가
/// </summary>


[Serializable]
public class CharacterData
{
    public CharacterType type;
    public bool isAvailable;
    public int stageSaved;
    public int stageLastPlayed;
    public ToolsLevel toolsLevel;
    public long selectionHistory; // 비트열 데이터
    public long piecesOfStory; // 비트열 데이터
}

[Serializable]
public enum CharacterType
{
    None,
    Mice,
    Laila,
    DrillGgabijo,
    Eily
}

[Serializable]
public struct ToolsLevel
{
    public int widthLevel;
    public int heightLevel;
    public int strengthLevel;
    public int luckLevel;
}

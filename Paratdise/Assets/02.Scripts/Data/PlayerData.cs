using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/28
/// 최종수정일 : 
/// 설명 : 
/// 
/// 플레이어 데이터
/// 닉네임 ( ID 로 향후 대체될 수 있음 )
/// 해금된 최고 스테이지
/// 진행중인 스테이지
/// 타입캡슐 리스트
/// 캐릭터 데이터 리스트
/// </summary>

[System.Serializable]
public class PlayerData
{
    public string nickName;
    public List<int> endingCardsData = new List<int>();
    public List<TimeCapsuleData> timeCapsulesData = new List<TimeCapsuleData>();
    public List<CharacterData> charactersData = new List<CharacterData>();

    public CharacterData GetCharacterData(CharacterType type)
    {
        foreach (var sub in charactersData)
        {
            if(sub.type == type)
                return sub;
        }
        return null;
    }

    public void SetCharacterData(CharacterData characterData)
    {
        for (int i = charactersData.Count - 1; i > -1; i--)
        {
            if (charactersData[i].type == characterData.type)
            {
                charactersData.RemoveAt(i);
                break;
            }
        }
        charactersData.Add(characterData);

    }

    public void SetStageLastPlayed(CharacterType type, int newStage)
    {
        foreach (var sub in charactersData)
        {
            if(sub.type == type)
            {
                sub.stageLastPlayed = newStage;
            }
        }
    }

    public int GetStageLastPlayed(CharacterType type)
    {
        foreach (var sub in charactersData)
        {
            if(sub.type == type)
                return sub.stageLastPlayed;
        }
        return 0;
    }

    public void SetStageSaved(CharacterType type, int newStage)
    {
        foreach (var sub in charactersData)
        {
            if (sub.type == type)
            {
                sub.stageSaved = newStage;
            }
        }
    }

    public int GetStageSaved(CharacterType type)
    {
        foreach (var sub in charactersData)
        {
            if (sub.type == type)
                return sub.stageSaved;
        }
        return 0;
    }

    /// <summary>
    /// 해당 타입의 캐릭터 데이터를 초기화함
    /// </summary>
    public void ResetCharacter(CharacterType type)
    {
        foreach (var sub in charactersData)
        {
            if (sub.type == type)
            {
                // 해당 캐릭터 디폴트
            }
        }
    }
}
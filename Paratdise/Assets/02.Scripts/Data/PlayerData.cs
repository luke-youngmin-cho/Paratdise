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
    public List<TimeCapsuleData> timeCapsulesData = new List<TimeCapsuleData>();
    public List<CharacterData> charactersData = new List<CharacterData>();

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

    public void ResetCharacter(CharacterType type)
    {

        foreach (var sub in charactersData)
        {

            if (sub.type == type)
            {
                ToolInfo realData = GameInfoController.toolInfos[1];//save와 load 할 실제 무기 데이터
                sub.stageSaved = 0;
                sub.stageLastPlayed = 0;
                ToolInfo targetTool = GameInfoController.GetToolByCharacter(type);
                realData = new ToolInfo(
                    targetTool.toolIndex, 
                    targetTool.toolName,
                    targetTool.toolHeight,
                    targetTool.toolWidth,
                    targetTool.luck,
                    targetTool.power,
                    targetTool.reinforceCount,
                    targetTool.toolType
                    );
            }
        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/03/28
/// ���������� : 
/// ���� : 
/// 
/// �÷��̾� ������
/// �г��� ( ID �� ���� ��ü�� �� ���� )
/// �رݵ� �ְ� ��������
/// �������� ��������
/// Ÿ��ĸ�� ����Ʈ
/// ĳ���� ������ ����Ʈ
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
                ToolInfo realData = GameInfoController.toolInfos[1];//save�� load �� ���� ���� ������
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
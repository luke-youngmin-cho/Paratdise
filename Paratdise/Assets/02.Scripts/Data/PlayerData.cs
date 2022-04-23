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
    /// �ش� Ÿ���� ĳ���� �����͸� �ʱ�ȭ��
    /// </summary>
    public void ResetCharacter(CharacterType type)
    {
        foreach (var sub in charactersData)
        {
            if (sub.type == type)
            {
                // �ش� ĳ���� ����Ʈ
            }
        }
    }
}
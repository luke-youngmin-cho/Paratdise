using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/03/28
/// ���������� : 2022/04/25
/// ���� : 
/// 
/// �÷��̾� ������
/// �г��� ( ID �� ���� ��ü�� �� ���� )
/// ����ī��, ���丮���� ������
/// ĳ���� ������ ����Ʈ
/// </summary>

[System.Serializable]
public class PlayerData
{
    public string nickName;
    public bool[] endingCards = new bool[100];
    public bool[] piecesOfStory = new bool[100];
    public List<CharacterData> charactersData = new List<CharacterData>();

    public void AddPieceOfStory(int storyIndex)
    {
        /*// �����迭
        if (storyIndex >= piecesOfStory.Length) 
        { 
            bool[] tmpArr = new bool[piecesOfStory.Length * 2];
            for (int i = 0; i < piecesOfStory.Length; i++)
                tmpArr[i] = piecesOfStory[i];
            piecesOfStory = new bool[tmpArr.Length];
            for (int i = 0; i < tmpArr.Length; i++)
                piecesOfStory[i] = tmpArr[i];
        }*/

        /* if (storyIndex < piecesOfStory.Count)
             piecesOfStory[storyIndex] = true;*/

    }

    /*public int[] GetPiecesOfStory()
    {
        int[] result = new int[sizeof(ulong)];
        for (int i = 0; i < result.Length; i++)
            result[i] = (int)(piecesOfStory >> i);
        return result;
    }*/

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
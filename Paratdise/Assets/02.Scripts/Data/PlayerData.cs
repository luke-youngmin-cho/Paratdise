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
    public int stageSaved;
    public int stageLastPlayed;
    public List<TimeCapsuleData> timeCapsulesData = new List<TimeCapsuleData>();
    public List<CharacterData> charactersData = new List<CharacterData>();
}
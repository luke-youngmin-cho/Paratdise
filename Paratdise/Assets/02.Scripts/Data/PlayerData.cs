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
/// �������� ��������
/// ĳ���� ������ ����Ʈ
/// �κ��丮 ������
/// </summary>
namespace YM
{
    [System.Serializable]
    public class PlayerData
    {
        public string nickName;
        public int stageSaved;
        public List<CharacterData> charactersData;
    }
}

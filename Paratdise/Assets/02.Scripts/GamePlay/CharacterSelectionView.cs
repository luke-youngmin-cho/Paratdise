using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/03/28
/// ���������� : 
/// ���� : 
/// 
/// ĳ���� ����â
/// ��ȹ����, ���� �����ɰ���
/// </summary>
namespace YM
{
    public class CharacterSelectionView : MonoBehaviour
    {
        public CharacterType characterSelected;

        //===============================================================================================
        //********************************** Public Methods *********************************************
        //===============================================================================================

        public void SelectCharacter(CharacterType type) =>
            characterSelected = type;


        // test
        private void Awake()
        {
            GameManager.SelectCharacter(characterSelected);
        }

    }

}
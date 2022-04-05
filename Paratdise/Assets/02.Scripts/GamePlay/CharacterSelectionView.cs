using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/28
/// 최종수정일 : 
/// 설명 : 
/// 
/// 캐릭터 선택창
/// 기획미정, 추후 수정될것임
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
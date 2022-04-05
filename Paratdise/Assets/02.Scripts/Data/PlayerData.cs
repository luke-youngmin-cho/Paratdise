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
/// 진행중인 스테이지
/// 캐릭터 데이터 리스트
/// 인벤토리 데이터
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

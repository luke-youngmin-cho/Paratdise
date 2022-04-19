using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/28
/// 최종수정일 : 
/// 설명 : 
/// 
/// 연출 단위 데이터. 
/// 아직 기획 미정으로 추후 수정될 것임
/// </summary>


//제가 만들어놓았던 story 클래스인데 
//이거 지우고 세인님이 새로 이런형태로 만들어주시면됨니다 

//여기서는 아마 삽화를 리스트나 배열로 저장해놓고 

//중요한거는 이걸 호출할 클래스가 어떻게 가져올수있을지 에요
//스토리를 호출할 클래스는 제가만든 StageManager 가 호출할거에요
public class Story
{
    public StoryExecuteType executeType;
    public StoryEffectType effect;
    public int stage;
    //public string contents; // 문자열은 안쓰신다니까 이거 필요없고
    public Sprite[] sprite; // 이걸로 삽화 순서대로 넣어놓으면  하나의 스토리를 만들수있게끔 해주시면되여 
    

}
// 언제실행할지
public enum StoryExecuteType
{
    None,
    StartOfStage,
    EndOfStage,
}
// 연출효과
public enum StoryEffectType
{
    Idle,
    FadeIn,
    FadeOut,
    Shake,
}

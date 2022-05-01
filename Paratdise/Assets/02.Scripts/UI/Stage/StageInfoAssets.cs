using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/10
/// 최종수정일 : 
/// 설명 : 
/// 
/// 스테이지 정보를 가져다 쓰기위한 클래스
/// </summary>

public class StageInfoAssets : MonoBehaviour
{
    private static StageInfoAssets _instance;
    public static StageInfoAssets instance
    {
        get
        {
            if (_instance == null)
                _instance = Instantiate(Resources.Load<StageInfoAssets>("Assets/StageInfoAssets"));
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    public List<StageInfo> stageInfos = new List<StageInfo>();

    public static StageInfo GetStageInfo(int stage) =>
        instance.stageInfos.Find(x => x.stage == stage);
     
}

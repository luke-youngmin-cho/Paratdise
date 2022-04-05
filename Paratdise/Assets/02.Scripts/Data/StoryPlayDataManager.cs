using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/28
/// 최종수정일 : 
/// 설명 : 
/// 
/// 연출 데이터를 생성, 저장 및 불러오기 
/// 아직 기획 미정으로 추후 수정될 예정임
/// </summary>
namespace YM
{
    public class StoryPlayDataManager
    {
        public static StoryPlayDataManager _instance;
        public static StoryPlayDataManager instance
        {
            get
            {
                if (_instance == null)
                    _instance = new StoryPlayDataManager();
                return _instance;
            }
        }

        public static StoryPlayData LoadStoryData(int stage)
        {
            StoryPlayData tmpData = null;


            string jsonPath = $"{Application.persistentDataPath}/StoryDatas/Story_{stage}.json";
            if (System.IO.File.Exists(jsonPath))
            {
                string jsonData = System.IO.File.ReadAllText(jsonPath);
                tmpData = JsonConvert.DeserializeObject<StoryPlayData>(jsonData);
            }
            return tmpData;
        }
    }

}
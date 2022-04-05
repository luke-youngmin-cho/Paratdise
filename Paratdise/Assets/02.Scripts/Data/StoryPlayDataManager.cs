using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/03/28
/// ���������� : 
/// ���� : 
/// 
/// ���� �����͸� ����, ���� �� �ҷ����� 
/// ���� ��ȹ �������� ���� ������ ������
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
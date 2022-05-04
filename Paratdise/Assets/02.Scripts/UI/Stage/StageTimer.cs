using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/05
/// 최종수정일 : 
/// 설명 : 
/// 
/// 스테이지 타이머 UI
/// </summary>
public class StageTimer : MonoBehaviour
{
    [SerializeField] private Text timeText;
    // Update is called once per frame
    void Update()
    {
        if (StageManager.instance != null)
            timeText.text = StageManager.instance.GetTimer().ToString();
    }
}

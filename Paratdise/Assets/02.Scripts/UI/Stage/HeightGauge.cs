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
/// 플레이어의 현재 높이를 나타내는 바
/// </summary>
public class HeightGauge : MonoBehaviour
{
    [SerializeField] private Slider slider;
    void Update()
    {
        if (Player.instance != null && 
            MapCreater.instance != null)
            slider.value = (Player.instance.transform.position.y - MapCreater.instance.mapTile_Start.position.y) / (MapCreater.mapHeight - 2);
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/04
/// 최종수정일 : 
/// 설명 : 
/// 
/// 맵의 정보를 가지고있는 클래스. 
/// 스테이지를 시작할때 해당 클래스의 멤버들을 가지고 MapCreater 가 맵을 구성.
/// </summary>

[CreateAssetMenu(fileName = "New MapInfo", menuName = "MapInfo/Create New MapInfo")]
public class MapInfo : ScriptableObject
{
    [Header("맵의 가로x세로 사이즈")]
    public Vector2 size;
    [Header("경계생성 알고리즘횟수. 클수록 경계 밀도 낮아짐")]
    public int algoNum;
    [Header("맵에서 유체가 차지하는 비율")]
    public int fluidPercents;
    [Header("맵에서 그외 장애물이 차지하는 비율")]
    public int obstaclePercents;
    [Header("경계 맵 타일 목록")]
    public List<GameObject> MapElements_Boundary; // 경계 맵타일 리스트
    [Header("기본 맵 타일 목록")]
    public List<GameObject> MapElements_Basic; // 기본 맵타일 리스트
    [Header("유체 맵 타일 목록")]
    public List<GameObject> MapElements_FluidBundle; // 유체 맵 요소 리스트
    [Header("방해물 맵 타일 목록")]
    public List<GameObject> MapElements_Obstacle; // 이벤트 요소 리스트
    [Header("시작 맵 타일")]
    public GameObject MapElement_Start; // 시작 맵타일 
    [Header("끝 맵 타일")]
    public GameObject MapElement_End; // 끝 맵타일 
    [Header("추격자")]
    public GameObject tracer;
    [Header("맵에 뿌려놓을 타임캡슐 개수")]
    public int timeCapsuleNum;
}
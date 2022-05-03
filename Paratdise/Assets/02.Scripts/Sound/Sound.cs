
using UnityEngine.Audio;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/03
/// 최종수정일 : 
/// 설명 : 
/// 
/// 개별 음원에 대한 정보
/// </summary>
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 1f)]
    public float pitch;
    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
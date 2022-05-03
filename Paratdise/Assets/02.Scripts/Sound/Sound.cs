
using UnityEngine.Audio;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/05/03
/// ���������� : 
/// ���� : 
/// 
/// ���� ������ ���� ����
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
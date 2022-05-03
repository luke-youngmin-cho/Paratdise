using UnityEngine.Audio;
using System;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/05/03
/// ���������� : 
/// ���� : 
/// 
/// ���� ��� �� ����� �ͼ��׷� ����
/// </summary>
public class AudioManager : MonoBehaviour
{
    static public AudioManager instance;
    public AudioMixerGroup bgmMixerGroup;
    public AudioMixerGroup sfxMixerGroup;
    Sound currentBGM;

    private void Awake()
    {
        if (instance != null) 
            Destroy(instance);
        instance = this;
        DontDestroyOnLoad(this);
    }

    public void PlayBGM(Sound sound)
    {
        if (sound == null) return;
        if (currentBGM != null)
            currentBGM.source.Stop();

        sound.source.Play();
        currentBGM = sound;
    }


    public void PlaySFX(Sound sound)
    {
        if (sound == null) return;
        sound.source.Play();
    }

}
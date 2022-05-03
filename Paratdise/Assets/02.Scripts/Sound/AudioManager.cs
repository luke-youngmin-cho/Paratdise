using UnityEngine.Audio;
using System;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/03
/// 최종수정일 : 
/// 설명 : 
/// 
/// 음원 재생 및 오디오 믹서그룹 관리
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
using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/03
/// 최종수정일 : 
/// 설명 : 
/// 
/// 효과음을 가져다쓰기위한 클래스
/// </summary>
public class SFXAssets : MonoBehaviour
{
    public static SFXAssets instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public List<Sound> sfxs = new List<Sound>();
    public AudioMixerGroup sfxMixerGroup;
    public static Sound GetSFX(string clipName) =>
        instance.sfxs.Find(x => x.clip.name == clipName);

    private void Start()
    {
        SetUp();
        StartCoroutine(E_MixerSetUp());
    }

    IEnumerator E_MixerSetUp()
    {
        yield return new WaitUntil(() => Settings.instance != null);
        sfxMixerGroup.audioMixer.SetFloat("VolumeExposed", Mathf.Log10(Settings.instance.Sound_SFXVolume) * 20);
    }
    private static void SetUp()
    {
        foreach (Sound sound in instance.sfxs)
        {
            sound.source = instance.gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.outputAudioMixerGroup = instance.sfxMixerGroup;
        }
    }
}
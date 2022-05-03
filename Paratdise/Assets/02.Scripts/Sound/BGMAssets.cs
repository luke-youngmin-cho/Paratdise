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
/// 배경음을 가져다쓰기위한 클래스
/// </summary>
public class BGMAssets : MonoBehaviour
{
    public static BGMAssets instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        SetUp();
        StartCoroutine(E_MixerSetUp());
    }

    IEnumerator E_MixerSetUp()
    {
        yield return new WaitUntil(() => Settings.instance != null);
        bgmMixerGroup.audioMixer.SetFloat("VolumeExposed", Mathf.Log10(Settings.instance.Sound_BGMVolume) * 20);
    }

    public List<Sound> bgms = new List<Sound>();
    public AudioMixerGroup bgmMixerGroup;
    public static Sound GetBGM(string clipName) =>
        instance.bgms.Find(x => x.clip.name == clipName);

    private static void SetUp()
    {
        foreach (Sound sound in instance.bgms)
        {
            sound.source = instance.gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.outputAudioMixerGroup = instance.bgmMixerGroup;
        }
    }
}
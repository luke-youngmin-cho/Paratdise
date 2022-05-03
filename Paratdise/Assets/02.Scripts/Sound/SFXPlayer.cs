using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/03
/// 최종수정일 : 
/// 설명 : 
/// 
/// 효과음 재생
/// </summary>
public class SFXPlayer : MonoBehaviour
{
    [SerializeField] private bool playOnStart;
    [SerializeField] private Sound sound;
    
    public void Play()
    {
        if (sound != null)
            sound.source.Play();
    }

    private void Start()
    {
        sound.source = AudioManager.instance.gameObject.AddComponent<AudioSource>();
        sound.source.clip = sound.clip;
        sound.source.volume = sound.volume;
        sound.source.pitch = sound.pitch;
        sound.source.loop = sound.loop;
        sound.source.outputAudioMixerGroup = AudioManager.instance.sfxMixerGroup;
        if (playOnStart)
            Play(); 
    }

    private void OnDisable()
    {
        if (sound != null && 
            sound.source != null)
            sound.source.Stop();
    }

    private void OnDestroy()
    {
        if (sound != null &&
            sound.source != null)
            sound.source.Stop();
    }
}
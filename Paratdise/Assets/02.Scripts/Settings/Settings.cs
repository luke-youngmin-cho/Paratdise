using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/03
/// 최종수정일 : 
/// 설명 : 
/// 
/// 환경설정 PlayerPrefs 에 저장하고 불러옴
/// </summary>
public class Settings : MonoBehaviour
{
    static public Settings instance;

    int isPlayerPrefsDataExist;
    float sound_BGMVolume;
    float sound_SFXVolume;
    float touch_JoystickSensitivity;
    int tutorial_FirstStage;
    int tutorial_Stage4Cleared;
    int tutorial_Stage6Playing;
    int tutorial_Stage8Playing;
    int tutorial_Stage13Playing;
    int tutorial_Diary;
    int tutorial_Upgrade;
    string nickName_LatestGuest;

    [SerializeField] Slider sound_BGMVolume_Slider;
    [SerializeField] Slider sound_SFXVolume_Slider;
    [SerializeField] Slider touch_JoystickSensitivity_Slider;

    [SerializeField] UnityEngine.Audio.AudioMixer bgmMixer;
    [SerializeField] UnityEngine.Audio.AudioMixer sfxMixer;
    public int PlayerPrefsDataExist
    {
        get
        {
            return PlayerPrefs.GetInt("PlayerPrefsDataExist", isPlayerPrefsDataExist);
        }
        set
        {
            isPlayerPrefsDataExist = value;
            PlayerPrefs.SetInt("PlayerPrefsDataExist", value);
        }
    }

    public float Sound_BGMVolume
    {
        get
        {
            return PlayerPrefs.GetFloat("Sound_BGMVolume", sound_BGMVolume);
        }
        set
        {
            sound_BGMVolume = value;
            bgmMixer.SetFloat("VolumeExposed", Mathf.Log10(sound_BGMVolume) * 20);
            PlayerPrefs.SetFloat("Sound_BGMVolume", sound_BGMVolume);
        }
    }
    public float Sound_SFXVolume
    {
        get
        {
            return PlayerPrefs.GetFloat("sound_SFXVolume", sound_SFXVolume);
        }
        set
        {
            sound_SFXVolume = value;
            sfxMixer.SetFloat("VolumeExposed", Mathf.Log10(sound_SFXVolume) * 20);
            PlayerPrefs.SetFloat("sound_SFXVolume", sound_SFXVolume);
        }
    }

    public float Touch_JoystickSensitivity
    {
        get
        {
            return PlayerPrefs.GetFloat("Touch_JoysticSensitivity", touch_JoystickSensitivity);
        }
        set
        {
            touch_JoystickSensitivity = value;
            PlayerPrefs.SetFloat("Touch_JoysticSensitivity", touch_JoystickSensitivity);
        }
    }

    public int Tutorial_FirstStage
    {
        get
        {
            return PlayerPrefs.GetInt("Tutorial_FirstStage", tutorial_FirstStage);
        }
        set
        {
            tutorial_FirstStage = value;
            PlayerPrefs.SetInt("Tutorial_FirstStage", tutorial_FirstStage);
        }
    }
    public int Tutorial_Stage4Cleared
    {
        get
        {
            return PlayerPrefs.GetInt("Tutorial_Stage4Cleared", tutorial_Stage4Cleared);
        }
        set
        {
            tutorial_FirstStage = value;
            PlayerPrefs.SetInt("Tutorial_Stage4Cleared", tutorial_Stage4Cleared);
        }
    }
    public int Tutorial_Stage6Playing
    {
        get
        {
            return PlayerPrefs.GetInt("Tutorial_Stage6Playing", tutorial_Stage6Playing);
        }
        set
        {
            tutorial_FirstStage = value;
            PlayerPrefs.SetInt("Tutorial_Stage6Playing", tutorial_Stage6Playing);
        }
    }
    public int Tutorial_Stage8Playing
    {
        get
        {
            return PlayerPrefs.GetInt("Tutorial_Stage8Playing", tutorial_Stage8Playing);
        }
        set
        {
            tutorial_FirstStage = value;
            PlayerPrefs.SetInt("Tutorial_Stage8Playing", tutorial_Stage8Playing);
        }
    }
    public int Tutorial_Stage13Playing
    {
        get
        {
            return PlayerPrefs.GetInt("Tutorial_Stage13Playing", tutorial_Stage13Playing);
        }
        set
        {
            tutorial_FirstStage = value;
            PlayerPrefs.SetInt("Tutorial_Stage13Playing", tutorial_Stage13Playing);
        }
    }
    public int Tutorial_Diary
    {
        get
        {
            return PlayerPrefs.GetInt("Tutorial_Diary", tutorial_Diary);
        }
        set
        {
            tutorial_Diary = value;
            PlayerPrefs.SetInt("Tutorial_Diary", tutorial_Diary);
        }
    }
    public int Tutorial_Upgrade
    {
        get
        {
            return PlayerPrefs.GetInt("Tutorial_Diary", tutorial_Upgrade);
        }
        set
        {
            tutorial_Upgrade = value;
            PlayerPrefs.SetInt("Tutorial_Diary", tutorial_Upgrade);
        }
    }

    public string NickName_LatestGuest
    {
        get
        {
            return PlayerPrefs.GetString("NickName_LatestGuest", nickName_LatestGuest);
        }
        set
        {
            nickName_LatestGuest = value;
            PlayerPrefs.SetString("NickName_LatestGuest", nickName_LatestGuest);
        }
    }

    public void LoadSavedData()
    {
        if (PlayerPrefsDataExist == 0) SetupAtVeryFirstTime();

        sound_BGMVolume_Slider.value = Sound_BGMVolume;
        sound_SFXVolume_Slider.value = Sound_SFXVolume;
        touch_JoystickSensitivity_Slider.value = Touch_JoystickSensitivity;

        sound_BGMVolume = Sound_BGMVolume;
        sound_SFXVolume = Sound_SFXVolume;
        touch_JoystickSensitivity = Touch_JoystickSensitivity;

        tutorial_Diary = Tutorial_Diary;
        tutorial_Upgrade = Tutorial_Upgrade;
        nickName_LatestGuest = NickName_LatestGuest;
    }

    public void OnValueChange_BGMSlider(float value) =>
        Sound_BGMVolume = value;

    public void OnValueChange_SFXSlider(float value) =>
        Sound_SFXVolume = value;

    private void SetupAtVeryFirstTime()
    {
        PlayerPrefsDataExist = 1;
        Sound_BGMVolume = 0.3f;
        Sound_SFXVolume = 0.3f;        
        Touch_JoystickSensitivity = 0.15f;
        NickName_LatestGuest = "";
    }
    public void SaveSettings()
    {
        PlayerPrefs.Save();
        //Debug.Log("Player prefs saved");
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }

    private void Start()
    {
        LoadSavedData();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        sound_BGMVolume_Slider.value = Sound_BGMVolume;
        sound_SFXVolume_Slider.value = Sound_SFXVolume;
    }
}
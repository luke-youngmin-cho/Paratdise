using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/03/28
/// 최종수정일 : 
/// 설명 : 
/// 
/// 캐릭터 타입에따라 캐릭터 에셋을 가져다 쓰기위한 클래스
/// </summary>

public class CharacterAssets : MonoBehaviour
{
    public static CharacterAssets _instance;
    public static CharacterAssets instance
    {
        get
        {
            if (_instance == null)
                _instance = Instantiate(Resources.Load<CharacterAssets>("Assets/CharacterAssets"));
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    [SerializeField] private List<GameObject> characterPrefabs = new List<GameObject>();

    public GameObject GetCharacter(CharacterType type) =>
        characterPrefabs.Find(x => x.name == type.ToString());

}
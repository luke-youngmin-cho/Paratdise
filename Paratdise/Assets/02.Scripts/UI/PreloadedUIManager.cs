using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/04
/// 최종수정일 : 
/// 설명 : 
/// 
/// 데이터 로딩과 적용이 필요한 UI들에 대한 매니저
/// </summary>
namespace YM
{
    public class PreloadedUIManager : MonoBehaviour
    {
        public static PreloadedUIManager instance;
        public static bool isReady = false;
        public GameObject inventoryView;
        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            inventoryView.SetActive(true);
        }

        private void Start()
        {
            StartCoroutine(E_Start());
        }

        IEnumerator E_Start()
        {
            yield return new WaitUntil(() => inventoryView.GetComponent<InventoryView>().isReady);
            inventoryView.SetActive(false);
            isReady = true;
        }


    }
}

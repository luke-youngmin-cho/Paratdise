using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Cysharp.Threading.Tasks;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/27
/// 최종수정일 : 
/// 설명 : 
/// 
/// Addressable 을 사용해서 비동기로 필요 에셋들을 비동기로 로드하는 클래스.
/// 반드시 Loading 씬으로 넘어가기 전에 인스턴스화가 되어야함.
/// 로드해야할 참조에셋들이 모두 로드되면 isLoaded 가 true 가 되니, 이 프로퍼티가 true 반환된 후에 시스템플로우 진행해야함.
/// </summary>
public class AssetsLoader : MonoBehaviour
{
    public static AssetsLoader instance;
    public static bool isLoaded
    {
        get
        {
            return (instance.assetsToLoad.Length == instance.instantiatedAccessoryObjects.Count) ? true : false;
        }
    }

    public AssetReference[] assetsToLoad;
    public AssetReference accessoryObjectToLoad;
        
    [SerializeField] private List<GameObject> instantiatedAccessoryObjects = new List<GameObject>();
    private GameObject instantiatedObject;

    //============================================================================
    //************************* Private Methods **********************************
    //============================================================================

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LoadAllAssets();
    }

    /// <summary>
    /// 게임시작전 로드되어야 하는 에셋들 모두 로드
    /// </summary>
    private void LoadAllAssets()
    {
        UniTask.Create(async () =>
        {
            await UniTask.WaitUntil(() => GameManager.instance != null);
            for (int i = 0; i < assetsToLoad.Length; i++)
            {
                //await assetsToLoad[i].LoadAssetAsync<GameObject>();
                Addressables.InstantiateAsync(assetsToLoad[i]).Completed += ObjectLoadDone;
            }
        });
    }

    /// <summary>
    /// Addressable 에셋 로드 완료되면 해당 에셋 생성
    /// </summary>
    private void ObjectLoadDone(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject loadedObject = obj.Result;
            Debug.Log($"오브젝트 로드 완료 {loadedObject}");

            instantiatedObject = Instantiate(loadedObject);
            Debug.Log($"오브젝트 인스턴스화 완료 {loadedObject}");


            if (accessoryObjectToLoad != null)
            {
                accessoryObjectToLoad.InstantiateAsync(instantiatedObject.transform).Completed += op =>
                {
                    if (op.Status == AsyncOperationStatus.Succeeded)
                    {
                        instantiatedAccessoryObjects.Add(op.Result);
                        Debug.Log("Accessory Object 로드와 생성 완료");
                    }
                };
            }
        }
    }
}
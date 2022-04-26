using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ۼ��� : ������
/// �����ۼ��� : 2022/04/10
/// ���������� : 
/// ���� : 
/// 
/// �ı������� ��Ÿ�� Ŭ����.
/// ü���� ���϶����� ����Ʈ�� �����ϸ�
/// ü���� 0�̵Ǹ� ��Ͽ��� ������ �������� ����ϰ� �ı���.
/// </summary>

public class MapTile_Destroyable : MapTile
{
    private float _hp;
    public float hp
    {
        set
        {
            _hp = value;

            //Debug.Log($"{this.name}: hp {value}");

            if (_hp < hpMax)
            {
                if (shakeCoroutine != null)
                    StopCoroutine(shakeCoroutine);
                shakeCoroutine = StartCoroutine(E_Shake());
            }

            float ratio = _hp / hpMax;

            if (ratio < 0.25)
            {
                if(crackEffect != null)
                    Destroy(crackEffect);
                crackEffect = Instantiate(MapTileEffectAssets.instance.GetEffectPrefab("crack_03"), transform);
            }   
            else if (ratio < 0.5)
            {
                if (crackEffect != null)
                    Destroy(crackEffect);
                crackEffect = Instantiate(MapTileEffectAssets.instance.GetEffectPrefab("crack_02"), transform);
            }   
            else if (ratio < 0.75)
            {
                if (crackEffect != null)
                    Destroy(crackEffect);
                crackEffect = Instantiate(MapTileEffectAssets.instance.GetEffectPrefab("crack_01"), transform);
            }
                

            if(_hp > 0)
            {
                if (hurtEffect != null)
                Instantiate(hurtEffect, transform.position, Quaternion.identity);
            }   
            else
            {
                DropRandomItem();
                if (destroyEffect != null)
                    Instantiate(destroyEffect, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
                ReturnToPool();
            }
        }
        get
        {
            return _hp;
        }
    }

    [SerializeField] float hpMax;
    GameObject crackEffect;


    [Header("�ǰ�/�ı��� ����Ʈ�� �ʿ��� ��� �����ڿ��� ���ǹٶ��ϴ�")]
    [SerializeField] private GameObject hurtEffect;
    [SerializeField] private GameObject destroyEffect;

    [System.Serializable]
    public class DropItemInfo
    {
        public GameObject itemPrefab;
        public float dropRatio; // 0~100;
    }
    [SerializeField] private List<DropItemInfo> dropItems = new List<DropItemInfo>();


    private SpriteRenderer spriteRenderer;
    private Coroutine shakeCoroutine = null;

    public override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        hp = hpMax;
    }

    private void DropRandomItem()
    {
        foreach (var item in dropItems)
        {
            float rand = Random.Range(0f, 100.0f);
            if (rand < item.dropRatio)
            {
                Instantiate(item.itemPrefab, transform.position, Quaternion.identity);
                break;
            }
        }
    }

    private IEnumerator E_Shake()
    {
        Vector3 startPos = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < 0.3f)
        {
            elapsedTime += 0.0167f;
            transform.position = startPos + Random.insideUnitSphere / 20;
            yield return null;
        }
        transform.position = startPos;
    }
}

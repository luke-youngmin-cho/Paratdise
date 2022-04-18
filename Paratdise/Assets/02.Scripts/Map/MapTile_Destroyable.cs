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
    private int _hp;
    public int hp
    {
        set
        {
            _hp = value;
            for (int i = 0; i < sprites.Length; i++)
            {
                if ((float)_hp / hpMax > (float) i / (sprites.Length))
                {
                    spriteRenderer.sprite = sprites[i];
                    break;
                }
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

    [SerializeField] int hpMax;
    [Header("������ ���ҿ� ���� ������� Sprite�� �߰��ϼ���.")]
    [SerializeField] private Sprite[] sprites;

    [Header("�ǰ�/�ı��� ����Ʈ�� �ʿ��� ��� �����ڿ��� ���ǹٶ��ϴ�")]
    [SerializeField] private GameObject hurtEffect;
    [SerializeField] private GameObject destroyEffect;

    [SerializeField]
    class DropItemInfo
    {
        public GameObject itemPrefab;
        public float dropRatio; // 0~100;
    }
    [SerializeField] private List<DropItemInfo> dropItems;


    private SpriteRenderer spriteRenderer;

    public override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        hp = hpMax;
        if(sprites.Length > 0)
            spriteRenderer.sprite = sprites[0];
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/04/10
/// 최종수정일 : 
/// 설명 : 
/// 
/// 파괴가능한 맵타일 클래스.
/// 체력이 깎일때마다 이펙트를 생성하며
/// 체력이 0이되면 목록에서 랜덤한 아이템을 드롭하고 파괴됨.
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
                Instantiate(hurtEffect,transform.position, Quaternion.identity);
            else
            {
                DropRandomItem();
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
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private GameObject hurtEffect;
    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private GameObject[] dropItems;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        hp = hpMax;
        spriteRenderer.sprite = sprites[0];
    }

    private void DropRandomItem()
    {
        int rand = Random.Range(0,dropItems.Length);
        Instantiate(dropItems[rand], transform.position, Quaternion.identity);
    }
}
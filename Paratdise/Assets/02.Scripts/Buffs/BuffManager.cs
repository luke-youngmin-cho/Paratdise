using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 작성자 : 조영민
/// 최초작성일 : 2022/05/02
/// 최종수정일 : 
/// 설명 : 
/// 
/// 버프를 플레이어에 적용시키거나 해제시키고 
/// 현재 적용된 버프의정보에 대해 관리함
/// </summary>
public class BuffManager : MonoBehaviour
{
    public static BuffManager instance;

    public Player player;
    public List<Buff> buffs = new List<Buff>(); // 모든 버프 목록

    /// <summary>
    /// 해당 버프에 대한 코루틴과 버프 발동 시간 
    /// </summary>
    public class BuffInfo
    {
        public Coroutine coroutine;
        public float generatedTime;
    }

    public Dictionary<Buff, BuffInfo> activatedBuffs = new Dictionary<Buff, BuffInfo>(); //발동된 버프 목록

    /// <summary>
    /// 버프목록에서 해당 타입 버프 검색해서 버프 코루틴 실행
    /// 이미 해당 버프 적용되고 있을때는 중첩되지 않음
    /// </summary>
    public static void ActiveBuff(BuffType buffType, BuffGenerator generator)
    {
        Buff tmpBuff = instance.buffs.Find(x => x.type == buffType);

        if (tmpBuff != null &&
            instance.activatedBuffs.ContainsKey(tmpBuff) == false)
        {
            Coroutine coroutine = instance.StartCoroutine(E_ActiveBuff(instance.player, tmpBuff, generator));
            BuffInfo tmpBuffInfo = new BuffInfo()
            {
                coroutine = coroutine,
                generatedTime = Time.time
            };
            instance.activatedBuffs.Add(tmpBuff, tmpBuffInfo);
            Debug.Log($"Activated buff {buffType}");
        }
    }

    /// <summary>
    /// 버프목록에서 해당타입 버프 검색해서 버프 코루틴 멈추고 삭제
    /// </summary>
    public static void DeactiveBuff(BuffType buffType, BuffGenerator generator)
    {
        Buff tmpBuff = instance.buffs.Find(x => x.type == buffType);

        if (tmpBuff != null &&
            instance.activatedBuffs.ContainsKey(tmpBuff) == true)
        {
            instance.StopCoroutine(instance.activatedBuffs[tmpBuff].coroutine);
            tmpBuff.OnDeactive(instance.player, generator);
            instance.activatedBuffs.Remove(tmpBuff);
            Debug.Log($"Deactivated buff {buffType}");
        }
    }

    /// <summary>
    /// 버프 적용을 위한 코루틴. 
    /// 버프 시작에 OnActive(), 적용중에는 OnDuration(), 끝날때 OnDeactive() 호출함.
    /// </summary>
    public static IEnumerator E_ActiveBuff(Player player, Buff buff, BuffGenerator generator)
    {
        buff.OnActive(player, generator);

        float timeMark = Time.time;
        while ((Time.time - timeMark < buff.duration) &&
               (player != null))
        {
            buff.OnDuration(player, generator);
            yield return null;
        }
        buff.OnDeactive(player, generator);
        instance.activatedBuffs.Remove(buff);
    }

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
        player = Player.instance;
    }
}
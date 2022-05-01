using UnityEngine;

public class Buff_Stun : Buff
{
    public float damage = 0;
    public float knockBackForce = 5f;

    public override void OnActive(Player player, BuffGenerator generator)
    {
        base.OnActive(player, generator);
        Vector2 forceVec = - PlayerStateMachineManager.instance.direction * knockBackForce;
        PlayerStateMachineManager.instance.KnockBack(forceVec);
        //PlayerStateMachineManager.instance.ChangeState(PlayerState.Hurt);
        PlayerStateMachineManager.instance.controllable = false;
        player.GetComponent<PlayerModelManager>().StartBlink(Color.white, duration, duration/4, false);
    }

    public override void OnDuration(Player player, BuffGenerator generator)
    {
        base.OnDuration(player, generator);
        
    }

    public override void OnDeactive(Player player, BuffGenerator generator)
    {
        base.OnDeactive(player, generator);
        PlayerStateMachineManager.instance.controllable = true;
    }
}
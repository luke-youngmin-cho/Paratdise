using UnityEngine;

public class Buff_Burn : Buff
{
    public float damage = 2;
    public float period = 2;
    private float timer;
    public override void OnActive(Player player, BuffGenerator generator)
    {
        base.OnActive(player, generator);
        timer = period;
        player.GetComponent<PlayerModelManager>().StartBlink(Color.red, duration, period, false);
    }

    public override void OnDuration(Player player, BuffGenerator generator)
    {
        base.OnDuration(player, generator);
        if (timer < 0)
            player.Hurt(damage);
        else
            timer -= Time.deltaTime;
    }

    public override void OnDeactive(Player player, BuffGenerator generator)
    {
        base.OnDeactive(player, generator);
    }
}
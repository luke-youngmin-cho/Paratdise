using UnityEngine;

public class Buff_Blind : Buff
{
    public GameObject blindPrefab;
    private GameObject blind;
    public override void OnActive(Player player, BuffGenerator generator)
    {
        base.OnActive(player, generator);
        blind = Instantiate(blindPrefab, player.transform.position, Quaternion.identity);
    }

    public override void OnDuration(Player player, BuffGenerator generator)
    {
        base.OnDuration(player, generator);
        blind.transform.position = player.transform.position;
    }

    public override void OnDeactive(Player player, BuffGenerator generator)
    {
        base.OnDeactive(player, generator);
        Destroy(blind);
    }
}
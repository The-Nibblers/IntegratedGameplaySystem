using UnityEngine;

public class BasePlayerStats : IPlayerStats
{
    public float GetFireRate() => 1f;

    public float GetMoveSpeed() => 5f;

    public float GetDamage() => 10f;

    public float GetMaxHealth() => 100f;
}

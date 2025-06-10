using UnityEngine;

public class BasePlayerStats : IPlayerStats
{
    /// <summary>
    /// TODO: balance player stats, cap player stats
    /// </summary>
    public float GetFireRate() => 1f;

    public float GetMoveSpeed() => 5f;

    public float GetDamage() => 10f;

    public float GetMaxHealth() => 100f;
}

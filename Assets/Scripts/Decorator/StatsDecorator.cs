using UnityEngine;

public abstract class StatsDecorator : IPlayerStats
{
    protected IPlayerStats _innerStats;

    public StatsDecorator(IPlayerStats innerStats)
    {
        _innerStats = innerStats;
    }
    public virtual float GetFireRate() => _innerStats.GetFireRate();

    public virtual float GetMoveSpeed() => _innerStats.GetMoveSpeed();

    public virtual float GetDamage() => _innerStats.GetDamage();

    public virtual float GetMaxHealth() => _innerStats.GetMaxHealth();
}

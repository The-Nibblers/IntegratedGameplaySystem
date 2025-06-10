using UnityEngine;

public class HealthModifier : StatsDecorator
{
    private float _multiplier;

    public HealthModifier(IPlayerStats innerStats, float multiplier) : base(innerStats)
    {
        _multiplier = _multiplier;
    }
    
    public override float GetMaxHealth() => _innerStats.GetMaxHealth() * _multiplier;
}

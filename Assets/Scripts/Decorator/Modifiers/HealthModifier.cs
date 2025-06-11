using UnityEngine;

public class HealthModifier : StatsDecorator
{
    private float _bonus;

    public HealthModifier(IPlayerStats innerStats, float multiplier) : base(innerStats)
    {
        _bonus = multiplier;
    }
    
    public override float GetMaxHealth() => _innerStats.GetMaxHealth() + _bonus;
}

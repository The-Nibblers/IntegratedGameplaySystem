using UnityEngine;

public class DamageModifier : StatsDecorator
{
    private float _multiplier;
    
    public DamageModifier(IPlayerStats innerStats, float multiplier) : base(innerStats)
    {
        _multiplier = multiplier;
    }
    
    public override float GetDamage() => _innerStats.GetDamage() * _multiplier;
}

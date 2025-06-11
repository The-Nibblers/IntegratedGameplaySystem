using UnityEngine;

public class DamageModifier : StatsDecorator
{
    private float _bonus;
    
    public DamageModifier(IPlayerStats innerStats, float multiplier) : base(innerStats)
    {
        _bonus = multiplier;
    }
    
    
    public override float GetDamage() => _innerStats.GetDamage() + _bonus;
}

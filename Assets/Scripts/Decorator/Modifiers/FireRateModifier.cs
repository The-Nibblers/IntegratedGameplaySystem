using UnityEngine;

public class FireRateModifier : StatsDecorator
{
    private float _bonus;

    public FireRateModifier(IPlayerStats innerStats, float multiplier) : base(innerStats)
    {
        _bonus = multiplier;
    }

    public override float GetFireRate() => _innerStats.GetFireRate() + _bonus;
}

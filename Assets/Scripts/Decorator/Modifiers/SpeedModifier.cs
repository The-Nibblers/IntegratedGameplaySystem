using UnityEngine;

public class SpeedModifier : StatsDecorator
{
    private float _bonus;
    private float _maxBonus = 15f;
    public SpeedModifier(IPlayerStats innerStats, float Bonus) : base(innerStats)
    {
        _bonus = Bonus;

        float moveSpeed = innerStats.GetMoveSpeed();
        if (moveSpeed + _bonus >= _maxBonus)
        {
            _bonus = 0;
        }
    }
    
    public override float GetMoveSpeed() => _innerStats.GetMoveSpeed() + _bonus;
   
}

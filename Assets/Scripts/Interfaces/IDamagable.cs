using UnityEngine;

public interface IDamagable
{
    float health { get; set; }
    void TryDamage(float amount);
    void takeDamage(float amount);
}

using UnityEngine;

public interface IDamagable
{
    int _health { get; set; }
    void TryDamage(int amount);
    void takeDamage(int amount);
}

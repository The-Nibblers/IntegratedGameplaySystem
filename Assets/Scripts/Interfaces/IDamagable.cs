using UnityEngine;

public interface IDamagable
{
    int health { get; set; }
    void TryDamage(int amount);
    void takeDamage(int amount);
}

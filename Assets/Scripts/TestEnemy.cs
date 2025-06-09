using System;
using UnityEngine;

public class TestEnemy : MonoBehaviour, IDamagable
{
    public int _health { get; set; }

    public void EnemyUpdate()
    {
        
    }
    
    public void TryDamage(int amount)
    {
        Debug.Log(amount);
    }

    public void takeDamage(int amount)
    {
        _health -= amount;
    }
}

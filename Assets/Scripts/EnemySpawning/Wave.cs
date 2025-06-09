using System.Collections.Generic;
using UnityEngine;

public class Wave
{
    public List<Enemy> Enemies { get; private set; }

    public Wave()
    {
        Enemies = new List<Enemy>();
    }

    public void AddEnemy(Enemy enemy)
    {
        Enemies.Add(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        Enemies.Remove(enemy);
        enemy = null;
    }
}

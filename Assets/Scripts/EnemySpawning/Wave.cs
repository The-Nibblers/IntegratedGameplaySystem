using System.Collections.Generic;
using UnityEngine;

public class Wave
{
    public List<Enemy> Enemies { get; private set; }
    
    public bool WaveIsActive { get; private set; }

    public Wave()
    {
        Enemies = new List<Enemy>();
    }

    public void AddEnemy(Enemy enemy)
    {
        Enemies.Add(enemy);
        WaveIsActive = true;
    }

    public void RemoveEnemy(Enemy enemy)
    {
        Enemies.Remove(enemy);
        enemy = null;

        if (Enemies.Count == 0)
        {
            WaveIsActive = false;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
public class Wave
{
    public List<Enemy> Enemies { get; private set; }
    private Dictionary<GameObject, Enemy> _enemyMap = new Dictionary<GameObject, Enemy>();
    
    private ItemDropper _itemDropper;
    public bool WaveIsActive { get; private set; }

    public Wave()
    {
        Enemies = new List<Enemy>();
    }

    public void AddEnemy(Enemy enemy)
    {
        Enemies.Add(enemy);
        _enemyMap[enemy.GameObject] = enemy;
        WaveIsActive = true;
    }

    public void RemoveEnemy(Enemy enemy)
    {
        Enemies.Remove(enemy);
        _enemyMap.Remove(enemy.GameObject);
        enemy = null;

        if (Enemies.Count == 0)
        {
            WaveIsActive = false;
        }
    }

    public Enemy GetEnemyByGameObject(GameObject obj)
    {
        if (_enemyMap.TryGetValue(obj, out var enemy))
        {
            return enemy;
        }
        return null;
    }
}

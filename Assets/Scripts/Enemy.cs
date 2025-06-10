using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Enemy : IDamagable
{
    
    /// <summary>
    /// TODO: refactor enemy collisiondetection (use overlapsphere)
    /// </summary>
    private NavMeshAgent _agent;
    private GameObject _playerObject;
    private Player _playerScript;
    private GameObject _enemyGameObject;
    public GameObject GameObject => _enemyGameObject;
    private Wave _wave;
    
    private int _damage;
    private int _speed;
    private int _hitRadius;
    private int _maxdistance;
    private int _maxHealth;
    
    private ItemDropper _itemDropper;
    
    private LayerMask _playerLayerMask = LayerMask.GetMask("Player");
    
    public float health { get; set; }

    public Enemy(NavMeshAgent agent, GameObject playerObject, Player playerScript, GameObject enemyGameObject, int damage, int speed, int hitRadius, int maxdistance, int maxHealth, Wave wave, ItemDropper itemDropper)
    {
        _agent = agent;
        _playerObject = playerObject;
        _playerScript = playerScript;
        _enemyGameObject = enemyGameObject;
        _damage = damage;
        _speed = speed;
        _hitRadius = hitRadius;
        _maxdistance = maxdistance;
        _maxHealth = maxHealth;
        _wave = wave;
        _itemDropper = itemDropper;
        
        _agent.speed = _speed;
        health = _maxHealth;
    }
    
    public void EnemyUpdate()
    {
        Move();
        CollisionDetection();
    }

    private void CollisionDetection()
    {
        RaycastHit hit;
        Vector3 direction = (_playerObject.transform.position - _enemyGameObject.transform.position).normalized;
        
        if (Physics.SphereCast(this._enemyGameObject.transform.position, _hitRadius,_playerObject.transform.position, out hit,_maxdistance))
        {
            if (((1 << hit.transform.gameObject.layer) & _playerLayerMask) == 0) return;
            _playerScript.TryDamage(_damage);
            
            //destroy gameobject
            _wave.RemoveEnemy(this);
            UnityEngine.Object.Destroy(_enemyGameObject);
        }
    }

    private void Move()
    {
        if (_agent.isOnNavMesh)
        {
            _agent.SetDestination(_playerObject.transform.position);   
        }
    }
    
    public void TryDamage(float amount)
    {
        takeDamage(amount);
    }

    public void takeDamage(float amount)
    {
        health -= amount;
        
        Debug.Log(amount + " damage taken" + health + " Current health");
        
        
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        int Chance = UnityEngine.Random.Range(0, 100);
        if (Chance < 30)
        {
            Vector3 SpawnPosition = new Vector3(_enemyGameObject.transform.position.x, _enemyGameObject.transform.position.y + 1, _enemyGameObject.transform.position.z);
            _itemDropper.SpawnItem(SpawnPosition); 
        }
        
        _wave.RemoveEnemy(this);
        UnityEngine.Object.Destroy(_enemyGameObject);
    }
}

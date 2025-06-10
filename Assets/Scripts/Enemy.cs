using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Enemy : IDamagable
{
    
    /// <summary>
    /// TODO: refactor enemy collisiondetection
    /// </summary>
    private NavMeshAgent _agent;
    private GameObject _playerObject;
    private Player _playerScript;
    private GameObject _enemyGameObject;
    private Wave _wave;
    
    private int _damage;
    private int _speed;
    private int _hitRadius;
    private int _maxdistance;
    private int _maxHealth;
    
    private LayerMask _playerLayerMask = LayerMask.GetMask("Player");
    
    public int health { get; set; }

    public Enemy(NavMeshAgent agent, GameObject playerObject, Player playerScript, GameObject enemyGameObject, int damage, int speed, int hitRadius, int maxdistance, int maxHealth, Wave wave)
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
    
    public void TryDamage(int amount)
    {
        takeDamage(amount);
    }

    public void takeDamage(int amount)
    {
        health -= amount;
        
        Debug.Log(amount + " damage taken" + health + " Current health");
        
        
        if (health <= 0)
        {
            _wave.RemoveEnemy(this);
            UnityEngine.Object.Destroy(_enemyGameObject);
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Enemy : IDamagable
{
    private NavMeshAgent _agent;
    private GameObject _playerObject;
    private Player _playerScript;
    private GameObject _enemyGameObject;
    
    private int _damage;
    private int _speed;
    private int _hitRadius;
    private int _maxdistance;
    private int _maxHealth;
    
    public int health { get; set; }

    public Enemy(NavMeshAgent agent, GameObject playerObject, Player playerScript, GameObject enemyGameObject, int damage, int speed, int hitRadius, int maxdistance, int maxHealth)
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
            Debug.Log("Hit");
            if (hit.transform.CompareTag("Player")) return;
            Debug.Log("player");
            _playerScript.TryDamage(_damage);
            
            //destroy gameobject
        }
    }

    private void Move()
    {
        _agent.SetDestination(_playerObject.transform.position);
    }
    
    public void TryDamage(int amount)
    {
        Debug.Log(amount);
    }

    public void takeDamage(int amount)
    {
        health -= amount;
        
        //destroy gameobject
    }
}

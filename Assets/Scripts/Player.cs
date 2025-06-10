using UnityEngine;

public class Player : IDamagable
{
    /// <summary>
    /// TODO: Health logic, fire cooldown
    /// </summary>
    private InputManager _inputManager;

    private ICommand _shootCommand;
    private IDirectionalCommand _moveCommand;
    private IDirectionalCommand _lookCommand;
    
    private GameObject _playerGameObject;
    private Camera _playerCamera;
    private float _lookSensitivity = 0.2f;
    
    private float _moveSpeed;
    private float _damage;
    private float _maxHealth;
    private float _fireRate;
    
    public float health { get; set; }

    private LayerMask _enemyMask = LayerMask.GetMask("Enemy");
    
    private Wave _currentWave;
    
    private IPlayerStats _playerStats;

    public Player(GameObject playerGameObject)
    {
        _shootCommand = new ShootCommand(this);
        _moveCommand = new MoveCommand(this);
        _lookCommand = new LookCommand(this);
        
        _inputManager = new InputManager(_shootCommand, _moveCommand, _lookCommand);
        
        _playerCamera = Camera.main;
        _playerGameObject = playerGameObject;

        _playerStats = new BasePlayerStats();

        _moveSpeed = _playerStats.GetMoveSpeed();
        _damage = _playerStats.GetDamage();
        _maxHealth = _playerStats.GetMaxHealth();
        _fireRate = _playerStats.GetFireRate();
        
    }
    
    public void playerUpdate()
    {
        _inputManager.HandleInput();
    }

    public void PlayerSetWave(Wave wave)
    {
        _currentWave = wave;
    }

    public void Move(Vector2 input)
    {
        Vector3 move = (_playerGameObject.transform.right * input.x + _playerGameObject.transform.forward * input.y);
        _playerGameObject.transform.position += move * (_moveSpeed * Time.deltaTime);
    }


    public void Look(Vector2 input)
    {
        // Rotate player left/right
        _playerGameObject.transform.Rotate(Vector3.up * (input.x * _lookSensitivity));

        // Rotate camera up/down (limit it so head no spin 360 like owl!)
        Vector3 currentEuler = _playerCamera.transform.localEulerAngles;
        float desiredPitch = currentEuler.x - input.y * _lookSensitivity;

        // Fix ugly Unity rotation wraparound
        if (desiredPitch > 180) desiredPitch -= 360;
        desiredPitch = Mathf.Clamp(desiredPitch, -80f, 80f); // Stop neck break

        _playerCamera.transform.localEulerAngles = new Vector3(desiredPitch, 0, 0);
    }

    public void Shoot()
    {
        if (Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.forward,
                out RaycastHit hit))
        {
            if (((1 << hit.transform.gameObject.layer) & _enemyMask) == 0)
            {
                // Not enemy layer, ignore hit or return
                return;
            }
            
            Debug.Log(hit.collider.gameObject.name);
            if (_currentWave == null) return;

            Debug.Log("wave isnt null");
            Enemy enemy = _currentWave.GetEnemyByGameObject(hit.transform.gameObject);
            if (enemy != null)
            {
                Debug.Log("Enemy isnt null");
                enemy.TryDamage(_damage);
            }
        }
    }
    //Decorator functions
    public void ChangeFireRate(float modifier)
    {
        _playerStats = new FireRateModifier(_playerStats, modifier);
        _fireRate = _playerStats.GetFireRate();
    }
    
    public void ChangeMaxHealth(float modifier)
    {
        _playerStats = new HealthModifier(_playerStats, modifier);
        _maxHealth = _playerStats.GetMaxHealth();
    }
    
    public void ChangeDamage(float modifier)
    {
        _playerStats = new DamageModifier(_playerStats, modifier);
        _damage = _playerStats.GetDamage();
    }
    
    public void ChangeSpeed(float modifier)
    {
        _playerStats = new SpeedModifier(_playerStats, modifier);
        _moveSpeed = _playerStats.GetMoveSpeed();
    }
    
    //health logic
    public void TryDamage(float amount)
    {
        takeDamage(amount);
    }

    public void takeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("dead");
    }
}

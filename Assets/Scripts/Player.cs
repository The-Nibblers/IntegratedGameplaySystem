using UnityEngine;
using UnityEngine.TextCore.Text;

public class Player : IDamagable
{
    private InputManager _inputManager;

    private ICommand _shootCommand;
    private ICommand _quitCommand;
    private IDirectionalCommand _moveCommand;
    private IDirectionalCommand _lookCommand;
    
    private GameObject _playerGameObject;
    private Camera _playerCamera;
    private float _lookSensitivity = 0.2f;
    
    private float _moveSpeed;
    private float _damage;
    private float _fireRate;
    
    private float _nextFireTime;
    
    private Animator _gunAnimator;
    
    public float health { get; set; }

    private LayerMask _enemyMask = LayerMask.GetMask("Enemy");
    
    private Wave _currentWave;
    
    private IPlayerStats _playerStats;
    
    private UIManager _uiManager;
    
    private CharacterController _playerCharacterController;

    private bool _canMove = true;
    private bool _canShoot = true;
    private bool _canLook = true;

    public Player(GameObject playerGameObject, UIManager uiManager, Animator gunAnimator)
    {
        _shootCommand = new ShootCommand(this);
        _moveCommand = new MoveCommand(this);
        _lookCommand = new LookCommand(this);
        _quitCommand = new QuitCommand(this);
        
        _inputManager = new InputManager(_shootCommand, _moveCommand, _lookCommand, _quitCommand);
        
        _playerCamera = Camera.main;
        _playerGameObject = playerGameObject;
        
        _uiManager = uiManager;

        _gunAnimator = gunAnimator;

        _playerStats = new BasePlayerStats();

        _moveSpeed = _playerStats.GetMoveSpeed();
        _damage = _playerStats.GetDamage();
        health = _playerStats.GetMaxHealth();
        _fireRate = _playerStats.GetFireRate();
        
        _uiManager.UpdateUi("FireRateUI", _fireRate);
        _uiManager.UpdateUi("HealthUI", health);
        _uiManager.UpdateUi("DamageUI", _damage);
        _uiManager.UpdateUi("SpeedUI", _moveSpeed);
        
        _playerCharacterController = playerGameObject.GetComponent<CharacterController>();
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
        if (!_canMove) return;
        
        Vector3 move = (_playerGameObject.transform.right * input.x + _playerGameObject.transform.forward * input.y);
        _playerCharacterController.Move(move * (_moveSpeed * Time.deltaTime));
    }


    public void Look(Vector2 input)
    {
        if (!_canLook) return;
        _playerGameObject.transform.Rotate(Vector3.up * (input.x * _lookSensitivity));
        
        Vector3 currentEuler = _playerCamera.transform.localEulerAngles;
        float desiredPitch = currentEuler.x - input.y * _lookSensitivity;
        
        if (desiredPitch > 180) desiredPitch -= 360;
        desiredPitch = Mathf.Clamp(desiredPitch, -80f, 80f);

        _playerCamera.transform.localEulerAngles = new Vector3(desiredPitch, 0, 0);
    }

    public void Shoot()
    {
        if (!_canShoot) return;
        if (Time.time < _nextFireTime) return;
        
        _gunAnimator.speed = _fireRate;
        _gunAnimator.SetTrigger("Shoot");

        _nextFireTime = Time.time + 1f / _fireRate;

        if (Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.forward,
                out RaycastHit hit))
        {
            if (((1 << hit.transform.gameObject.layer) & _enemyMask) == 0)
                return;
            
            if (_currentWave == null) return;
            
            Enemy enemy = _currentWave.GetEnemyByGameObject(hit.transform.gameObject);
            if (enemy != null)
                enemy.TryDamage(_damage);
        }
        
    }
    
    //Decorator functions
    public void ChangeFireRate(float modifier)
    {
        _playerStats = new FireRateModifier(_playerStats, modifier);
        _fireRate = _playerStats.GetFireRate();
        _uiManager.UpdateUi("FireRateUI", _fireRate);
    }
    
    public void ChangeMaxHealth(float modifier)
    {
        _playerStats = new HealthModifier(_playerStats, modifier);
        health = _playerStats.GetMaxHealth();
        _uiManager.UpdateUi("HealthUI", health);
        health = health;
    }
    
    public void ChangeDamage(float modifier)
    {
        _playerStats = new DamageModifier(_playerStats, modifier);
        _damage = _playerStats.GetDamage();
        _uiManager.UpdateUi("DamageUI", _damage);
    }
    
    public void ChangeSpeed(float modifier)
    {
        _playerStats = new SpeedModifier(_playerStats, modifier);
        _moveSpeed = _playerStats.GetMoveSpeed();
        _uiManager.UpdateUi("SpeedUI", _moveSpeed);
    }
    
    //health logic
    public void TryDamage(float amount)
    {
        takeDamage(amount);
    }

    public void takeDamage(float amount)
    {
        health -= amount;
        _uiManager.UpdateUi("HealthUI", health);

        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    private void Die()
    {
        _canShoot = false;
        _canMove = false;
        _canLook = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _uiManager.EnableDeathUI();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

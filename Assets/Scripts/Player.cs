using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    private InputManager _inputManager;

    private ICommand _shootCommand;
    private IDirectionalCommand _moveCommand;
    private IDirectionalCommand _lookCommand;
    
    private GameObject _playerGameObject;
    private Camera _playerCamera;
    
    private float _moveSpeed = 5f;
    private float _lookSensitivity = 0.2f;
    private int _damage;
    
    public int health { get; set; }

    private LayerMask EnemyMask = LayerMask.GetMask("Enemy");

    public Player(GameObject playerGameObject)
    {
        _shootCommand = new ShootCommand(this);
        _moveCommand = new MoveCommand(this);
        _lookCommand = new LookCommand(this);
        
        _inputManager = new InputManager(_shootCommand, _moveCommand, _lookCommand);
        
        _playerCamera = Camera.main;
        _playerGameObject = playerGameObject;
    }
    
    public void playerUpdate()
    {
        _inputManager.HandleInput();
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
        if (Physics.Raycast(_playerGameObject.transform.position, _playerGameObject.transform.forward,
                out RaycastHit hit,EnemyMask))
        {
            IDamagable damagable = hit.transform.GetComponent<IDamagable>();
            if (damagable != null)
            {
                Debug.Log(hit.transform.name);
                damagable.TryDamage(_damage);
            }
        }
    }
    public void TryDamage(int amount)
    {
        Debug.Log("player damaged" + amount);
    }

    public void takeDamage(int amount)
    {
        throw new System.NotImplementedException();
    }
}

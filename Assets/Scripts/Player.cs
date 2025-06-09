using UnityEngine;

public class Player
{
    private InputManager _inputManager;

    private ICommand _shootCommand;
    private IDirectionalCommand _moveCommand;
    private IDirectionalCommand _lookCommand;

    public Player()
    {
        _shootCommand = new ShootCommand(this);
        _moveCommand = new MoveCommand(this);
        _lookCommand = new MoveCommand(this);
        
        _inputManager = new InputManager(_shootCommand, _moveCommand, _lookCommand);
    }
    
    public void playerUpdate()
    {
        _inputManager.HandleInput();
    }

    public void Move(Vector3 direction)
    {
        
    }

    public void Look(Vector3 direction)
    {
        
    }

    public void Shoot()
    {
        Debug.Log("SHOOT");
    }
}

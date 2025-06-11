using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager
{
    private ICommand _shootCommand;
    private ICommand _quitCommand;
    private IDirectionalCommand _moveCommand;
    private IDirectionalCommand _lookCommand;
    
    private PlayerInputActions _inputActions;
    private Vector2 _MoveInput;
    private Vector2 _LookInput;

    public InputManager(ICommand shootCommand, IDirectionalCommand moveCommand, IDirectionalCommand lookCommand, ICommand quitCommand)
    {
        _shootCommand = shootCommand;
        _moveCommand = moveCommand;
        _lookCommand = lookCommand;
        _quitCommand = quitCommand;
        
        _inputActions = new PlayerInputActions();
        _inputActions.Enable();
        
        _inputActions.Gameplay.Move.performed += ctx => _MoveInput = ctx.ReadValue<Vector2>();
        _inputActions.Gameplay.Move.canceled += ctx => _MoveInput = Vector2.zero;

        _inputActions.Gameplay.Look.performed += ctx => _LookInput = ctx.ReadValue<Vector2>();
        _inputActions.Gameplay.Look.canceled += ctx => _LookInput = Vector2.zero;

        _inputActions.Gameplay.Shoot.performed += ctx => _shootCommand.Execute();
        _inputActions.Gameplay.Quit.performed += ctx => _quitCommand.Execute();
    }

    public void HandleInput()
    {
        _moveCommand.Execute(_MoveInput);
        _lookCommand.Execute(_LookInput);
    }
}
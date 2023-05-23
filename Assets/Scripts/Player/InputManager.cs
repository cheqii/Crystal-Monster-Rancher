using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InputManager : MonoBehaviour
{
    [Header("Input System")]
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions _onFoot;
    
    [Header("Player Scripts")]
    private PlayerMovement _playerMovement; // player movement script like jump and move
    private PlayerLook _playerLook; // player look script like look up and down and left and right

    private void Awake()
    {
        playerInput = new PlayerInput();
        _onFoot = playerInput.OnFoot;
        _playerMovement = GetComponent<PlayerMovement>();
        _playerLook = GetComponent<PlayerLook>();
        
        // player called sprint function (press shift to sprint and press again to stop sprinting)
        _onFoot.Sprint.performed += ctx => _playerMovement.Sprint();
        // added this for the sprint button is released, call back (ctx) the sprint function from PlayerMovement Script
        _onFoot.Sprint.canceled += ctx => _playerMovement.Sprint();
        
        // anytime that onFoot.jump is performed, call back (ctx) the jump function from PlayerMovement Script
        _onFoot.Jump.performed += ctx => _playerMovement.Jump();
    }
    
    void FixedUpdate()
    { 
        _playerMovement.ProcessMove(_onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        _playerLook.ProcessLook(_onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        _onFoot.Enable();
    }
    
    void OnDisable()
    {
        _onFoot.Disable();
    }
}

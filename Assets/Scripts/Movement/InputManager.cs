using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;

    private PlayerInput.OnFootActions onFoot;

    private PlayerMovement _playerMovement;
    private PlayerLook _playerLook;

    private void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        _playerMovement = GetComponent<PlayerMovement>();
        _playerLook = GetComponent<PlayerLook>();

        // anytime that onfoot,jump is performed, call back (ctx) the jump function
        onFoot.Jump.performed += ctx => _playerMovement.Jump();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _playerMovement.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        _playerLook.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }
    
    void OnDisable()
    {
        onFoot.Disable();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public bool isSprinting;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float gravityValue = -9.8f;
    [SerializeField] private float jumpHeight = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // lock cursor to the center of the screen
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        isSprinting = speed == sprintSpeed;
    }

    #region -Player Movement-

    public void ProcessMove(Vector2 input)  // receives input from InputManager.cs and apply it to the character controller component
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        Vector3 move = transform.right * moveDirection.x + transform.forward * moveDirection.z; // calculate movement direction
        controller.Move(move * speed * Time.deltaTime);
        
        // apply gravity
        playerVelocity.y += gravityValue * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0) playerVelocity.y = -2f;
        
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if(isGrounded) playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        Debug.Log("Jumped");
    }
    
    public void Sprint()
    {
        if(isGrounded && !isSprinting) speed = sprintSpeed;
        else if (isSprinting) speed = 5f;
    }

    #endregion

}

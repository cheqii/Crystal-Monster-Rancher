using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    public Camera Cam
    {
        get => _camera;
        set => _camera = value;
    }

    private float _xRotatation = 0f;
    
    [SerializeField] private float xSensitivity = 30f;
    [SerializeField] private float ySensitivity = 30f;

    #region -Inventory Panel-

    public GameObject inventoryPanel;
    public GameObject storagePanel;

    #endregion

    public GameObject crosshair;

    private void Update()
    {
        CursorControl();
    }

    public void ProcessLook(Vector2 input)
    {
        if(inventoryPanel.activeInHierarchy || storagePanel.activeInHierarchy) return; // if inventory is open, don't rotate camera
        
        float mouseX = input.x;
        float mouseY = input.y;
        
        // calculate camera rotation by mouse input
        _xRotatation -= (mouseY * ySensitivity * Time.deltaTime);
        _xRotatation = Mathf.Clamp(_xRotatation, -80f, 80f);
        
        // apply this to camera transform
        _camera.transform.localRotation = Quaternion.Euler(_xRotatation, 0f, 0f);
        
        // rotate player body by mouse input
        transform.Rotate(Vector3.up * (mouseX * xSensitivity * Time.deltaTime));
    }

    void CursorControl()
    {
        if(inventoryPanel.activeInHierarchy || storagePanel.activeInHierarchy) // if inventory is open, unlock cursor
        {
            crosshair.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else // if inventory is closed, lock cursor
        {
            crosshair.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}

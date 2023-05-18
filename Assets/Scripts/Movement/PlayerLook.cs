using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private float xRotatation = 0f;
    
    [SerializeField] private float xSensitivity = 30f;
    [SerializeField] private float ySensitivity = 30f;


    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        
        // calculate camera rotation by mouse input
        xRotatation -= (mouseY * ySensitivity * Time.deltaTime);
        xRotatation = Mathf.Clamp(xRotatation, -80f, 80f);
        
        // apply this to camera transform
        _camera.transform.localRotation = Quaternion.Euler(xRotatation, 0f, 0f);
        
        // rotate player body by mouse input
        transform.Rotate(Vector3.up * (mouseX * xSensitivity * Time.deltaTime));
    }
}

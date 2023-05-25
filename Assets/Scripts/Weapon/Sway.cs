using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{
    
    [Header("sway")] 
    [SerializeField] private float swayMultiply;
    [SerializeField] private float smooth;
    


    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiply;
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiply;

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotationSway = rotationX * rotationY;
        
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotationSway,smooth * Time.deltaTime);
    }
}

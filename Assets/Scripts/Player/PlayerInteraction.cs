using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private Camera _camera;

    [SerializeField] private float distance = 3f;
    [SerializeField] private SpriteRenderer crosshair;
    // [SerializeField] private LayerMask _layerMask;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<PlayerLook>().Cam;
    }

    // Update is called once per frame
    void Update()
    {
        // create a ray at the center of the camera, shooting outwards
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        
        // draw the ray in the scene view
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
        
        // check if the ray hits something
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.collider != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Shoot!");
                    Debug.Log("Hit: " + hit.collider.name);
                }
            }
        }
    }
}

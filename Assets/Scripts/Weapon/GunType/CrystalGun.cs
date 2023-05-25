using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalGun : Gun, IShoot
{
    [SerializeField] private CrystalBullet crystalBullet;
    [SerializeField] private Transform shootPoint;

    void Start()
    {
        _Camera = GetComponentInParent<PlayerLook>().Cam;
    }
    void Update()
    {
        if(GunHold != GunType.CrystalGun) return;
        Shoot();
    }

    #region -Crystal Shoot-

    public void Shoot()
    {
        if(GunHold != GunType.CrystalGun) return;
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 rayOrigin = _Camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f)); // center of the camera to create ray origin
            RaycastHit hit;
            
            if (Physics.Raycast(rayOrigin, _Camera.transform.forward, out hit, Distance))
            {
                Debug.Log("Change to Crystal");
            }
            
            crystalBullet.Move(shootPoint, hit.point);
        }
    }

    #endregion

}

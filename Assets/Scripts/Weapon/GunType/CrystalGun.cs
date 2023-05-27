using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalGun : Gun, IShoot
{
    [SerializeField] private CrystalBullet crystalBullet;
    [SerializeField] private Transform shootPoint;
    
    private GameObject mainInventory;
    private GameObject storageInventory;
    void Start()
    {
        _Camera = GetComponentInParent<PlayerLook>().Cam;
    }
    void Update()
    {
        Shoot();
    }

    #region -Crystal Shoot-

    public void Shoot()
    {
        if(GunHold != GunType.CrystalGun) return;
        if (Input.GetMouseButtonDown(0))
        {
            // if inventory or storage is open, can't shoot a gun
            var main = GameObject.FindGameObjectWithTag("MainInventory");
            var storage = GameObject.FindGameObjectWithTag("Storage");
            mainInventory = main;
            storageInventory = storage;
            
            if(main) if(mainInventory.activeSelf) return;
            if(storage) if(storageInventory.activeSelf) return;
            
            Vector3 rayOrigin = _Camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f)); // center of the camera to create ray origin
            RaycastHit hit;
            
            if (Physics.Raycast(rayOrigin, _Camera.transform.forward, out hit, Distance))
            {
                Debug.Log("Change to Crystal");
            }
            else
            {
                if(crystalBullet.gameObject.activeInHierarchy) Destroy(crystalBullet);
            }
            
            crystalBullet.Move(shootPoint, shootPoint.position + (_Camera.transform.forward * Distance));
        }
    }

    #endregion

}

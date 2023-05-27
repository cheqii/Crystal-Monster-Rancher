using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalGun : Gun, IShoot
{
    [SerializeField] private CrystalBullet crystalBullet;
    [SerializeField] private CollectCrystal collectCrystalBullet;

    [SerializeField] private Transform shootPoint;
    public ParticleSystem crystalWaveParticle;
    public ParticleSystem muzzleFlashParticle;

    private GameObject mainInventory;
    private GameObject storageInventory;
    void Start()
    {
        _Camera = GetComponentInParent<PlayerLook>().Cam;
    }
    void Update()
    {
        base.Update();
        Shoot();
    }

    #region -Crystal Shoot-

    public void Shoot()
    {
        if(GunHold != GunType.CrystalGun) return;
        if (Input.GetMouseButtonDown(0))
        {
            muzzleFlashParticle.Play();

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
        
        if (Input.GetMouseButtonDown(1))
        {
            crystalWaveParticle.Play();
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
                if(collectCrystalBullet.gameObject.activeInHierarchy) Destroy(collectCrystalBullet);
            }
            
            collectCrystalBullet.Move(shootPoint, shootPoint.position + (_Camera.transform.forward * Distance));
        }
    }

    #endregion

}

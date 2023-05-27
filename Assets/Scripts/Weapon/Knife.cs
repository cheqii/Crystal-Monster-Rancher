using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Knife : Weapon
{
    #region -Declared Variables-
    
    [SerializeField] private int recoil;
    [SerializeField] private float knifeDelay;
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;
    [SerializeField] private float distance = 1f;

    [Header("On Hold")]
    [SerializeField] private GunType hold = GunType.None;
    
    private Camera _camera;
    private float nextStab;

    private GameObject mainInventory;
    private GameObject storageInventory;
    
    #endregion

    #region -Unity Event Functions-

    private void Start()
    {
        _camera = GetComponentInParent<PlayerLook>().Cam;
    }

    private void Update()
    {
        KnifeStab();
    }

    #endregion

    #region -Knife-

    void KnifeStab()
    {
        nextStab += Time.deltaTime;
        if (hold != GunType.None) return;
        if (Input.GetMouseButtonDown(0) && nextStab > knifeDelay)
        {
            // if inventory or storage is open, can't stab a knife
            var main = GameObject.FindGameObjectWithTag("MainInventory");
            var storage = GameObject.FindGameObjectWithTag("Storage");
            mainInventory = main;
            storageInventory = storage;
            
            if(main) if(mainInventory.activeSelf) return;
            if(storage) if(storageInventory.activeSelf) return;
            
            Vector3 rayOrigin = _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            
            if (Physics.Raycast(rayOrigin, _camera.transform.forward, out hit, distance))
            {   
                KnifeDamage();
            }

            StartCoroutine(KnifeDelay());
        }
    }
    
    void KnifeDamage()
    {
        int damage = Random.Range(minDamage, maxDamage);
        Debug.Log("Knife Damage: " + damage);
    }

    IEnumerator KnifeDelay()
    {
        nextStab = 0;
        yield return new WaitForSeconds(knifeDelay);
    }
    
    #endregion
}

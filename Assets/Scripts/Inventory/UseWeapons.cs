using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseWeapons : MonoBehaviour
{
    #region -All Weapons-

    [Header("Weapons")]
    [SerializeField] private GameObject laserGun;
    [SerializeField] private GameObject crystalGun;
    [SerializeField] private GameObject gravityGun;
    [SerializeField] private GameObject knife;

    [Header("ItemOnObject")]
    private ItemOnObject _itemOnObject; // for checking items id
    private ToggleItems _toggleItems;

    // private bool toggle;
    
    #endregion
    

    string GetItemsData()
    {
        _itemOnObject = GetComponent<ItemOnObject>();
        for (int i = 0; i < this.transform.parent.parent.GetChild(i).childCount; i++)
        {
            if (this.transform.parent.parent.GetChild(i).childCount == 0) return "";
            if (_itemOnObject.itemInventory.itemType == ItemType.UFPS_Weapon)
            {
                Debug.Log(_itemOnObject.itemInventory.itemName);
                return _itemOnObject.itemInventory.itemName;
            }
        }
        return "";
    }
    
    public void ActivateGun()
    {
        GetItemsData();
        
        switch (_itemOnObject.itemInventory.itemName)
        {
            case "Laser Gun":
            {
                if (laserGun.activeSelf) return;
                if (!laserGun.activeSelf && !crystalGun.activeSelf && !gravityGun.activeSelf 
                    && !knife.activeSelf)
                    laserGun.SetActive(true);
                SwitchWeapons(crystalGun, laserGun);
                SwitchWeapons(gravityGun, laserGun);
                SwitchWeapons(knife, laserGun);
                break;
            }
            case "Crystal Gun":
            {
                if (crystalGun.activeSelf) return;
                if (!laserGun.activeSelf && !crystalGun.activeSelf && !gravityGun.activeSelf 
                    && !knife.activeSelf)
                    crystalGun.SetActive(true);
                SwitchWeapons(laserGun, crystalGun);
                SwitchWeapons(gravityGun, crystalGun);
                SwitchWeapons(knife, crystalGun);
                break;
            }
            case "Gravity Gun":
            {
                if(gravityGun.activeSelf) return;
                if (!laserGun.activeSelf && !crystalGun.activeSelf && !gravityGun.activeSelf 
                    && !knife.activeSelf)
                    gravityGun.SetActive(true);
                SwitchWeapons(laserGun, gravityGun);
                SwitchWeapons(crystalGun,gravityGun);
                SwitchWeapons(knife, gravityGun);
                break;
            }
            case "Knife":
            {
                if(knife.activeSelf) return;
                if (!laserGun.activeSelf && !crystalGun.activeSelf && !gravityGun.activeSelf 
                    && !knife.activeSelf)
                    knife.SetActive(true);
                SwitchWeapons(laserGun, knife);
                SwitchWeapons(crystalGun, knife);
                SwitchWeapons(gravityGun, knife);
                break;
            }
            default:
            {
                Debug.Log("null why get default answer????");
                break;
            }
        }
    }

    public void SwitchWeapons(GameObject holdWeapon, GameObject newWeapon)
    {
        if(!holdWeapon.activeInHierarchy) return; // if holdGun didn't active in hierarchy then return 
        
        // if holdGun has active in hierarchy, set holdGun = false and active newGun
        holdWeapon.SetActive(false);
        newWeapon.SetActive(true);
    }
    

}

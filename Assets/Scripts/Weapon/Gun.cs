using UnityEngine;

public enum GunType
{
    WaterGun,
    GravityGun,
    LaserGun,
    CrystalGun
}

public class Gun : Weapon
{
    #region -Declared Variables-

    [Header("Gun Data")]
    [SerializeField] private int ammo;
    [SerializeField] private int recoil;
    [SerializeField] private float ammoDelay;
    
    [Header("Gun Type")]
    [SerializeField] private GunType gunType = GunType.LaserGun;

    #endregion

    #region -Unity Function-

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    #endregion
    
    
}

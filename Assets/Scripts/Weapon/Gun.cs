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

    [SerializeField] private int ammo;
    [SerializeField] private int recoil;
    [SerializeField] private float ammoDelay;

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

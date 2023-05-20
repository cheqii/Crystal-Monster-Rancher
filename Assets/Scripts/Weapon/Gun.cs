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

    protected Camera _Camera;
    [SerializeField] private float distance = 3f;

    public float Distance
    {
        get => distance;
        set => distance = value;
    }
    
    [Header("Gun Data")]
    [SerializeField] private int ammo;
    public int Ammo
    {
        get => ammo;
        set => ammo = value;
    }
    
    [SerializeField] private int recoil;
    public int Recoil
    {
        get => recoil;
        set => recoil = value;
    }
    
    [SerializeField] private float ammoDelay;
    public float AmmoDelay
    {
        get => ammoDelay;
        set => ammoDelay = value;
    }
    
    [Header("Gun Type")]
    [SerializeField] private GunType gunHold = GunType.LaserGun;

    public GunType GunHold
    {
        get => gunHold;
        set => gunHold = value;
    }

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

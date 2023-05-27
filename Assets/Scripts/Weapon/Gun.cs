using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public enum GunType
{
    None,
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


    public TwoBoneIKConstraint rig1;
    public TwoBoneIKConstraint rig2;
    
    public Transform rig1_target;
    public Transform rig1_hint;

    public Transform rig2_target;
    public Transform rig2_hint;

    #endregion

    #region -Unity Function-
    

    public void Update()
    {
        rig1.data.target.position = rig1_target.position;
        rig1.data.target.rotation = rig1_target.rotation;
        
        rig1.data.hint.position = rig1_hint.position;

        rig2.data.target.position = rig2_target.position;
        rig2.data.target.rotation = rig2_target.rotation;
        rig2.data.hint.position = rig2_hint.position;
    }

    #endregion


}

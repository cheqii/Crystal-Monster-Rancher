using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon
{
    #region -Declared Variables-

    [SerializeField] private int recoil;
    [SerializeField] private float knifeDelay;
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;

    #endregion
}

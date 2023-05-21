using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWander
{
    public bool isWander { get; set; }

    public float wanderRadius { get; set; }
    public float wanderTimer { get; set; }
    public float wanderDelay { get; set; }

    public void SetIsWander(bool _bool);
}

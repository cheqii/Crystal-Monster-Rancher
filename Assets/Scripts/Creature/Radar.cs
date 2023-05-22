using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    private Creature _creature;


    private void Awake()
    {
        _creature = transform.parent.GetComponent<Creature>();
    }

    private void OnTriggerStay(Collider col)
    {
        _creature.Radar(col.transform);
    }
    
    private void OnTriggerExit(Collider col)
    {
        _creature.StopWalking(col.transform);
    }
}

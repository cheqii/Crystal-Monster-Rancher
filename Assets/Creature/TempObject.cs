using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempObject : MonoBehaviour
{
    public GameObject SoulCrystal;
    public GameObject CrystalParticle;

    
    public static TempObject Instance { get; private set; }
    
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
    
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }


    public void DestroyDelay(GameObject gameObject,float delay)
    {
        Destroy(gameObject,delay);
    }
    
    
    
}

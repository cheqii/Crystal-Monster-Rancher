using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrowable
{
    
    public float MaxStomach { get; set; }
    
    public float CurrentStomach { get; set; }

    public float FoodIngestDelay { get; set; }
    public float SizeLimit  { get; set; }
    public bool NeedFood { get; set; }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrowable
{
    public float GrowRate { get; set; }
    public float SizeLimit  { get; set; }
    public float EatDelay { get; set; }
    public bool NeedFood { get; set; }



}

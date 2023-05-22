using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FoodChoice", order = 1)]
public class FoodChoice : ScriptableObject
{
    [Range(0,11) ,Header("Set value to 11 to disable this food")]
    public float EatHumanWhen;
    [HideInInspector]
    public bool Human;

    [Range(0,11)]
    public float EatDragonWhen;
    [HideInInspector]
    public bool Dragon;
    
    [Range(0,11)]
    public float EatPlantWhen;
    [HideInInspector]
    public bool Plant;

    [Range(0,11)]
    public float EatCrystalWhen;
    [HideInInspector]
    public bool Crystal;
}
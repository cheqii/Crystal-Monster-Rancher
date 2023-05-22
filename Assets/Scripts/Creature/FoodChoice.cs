using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FoodChoice", order = 1)]
public class FoodChoice : ScriptableObject
{
    [Range(0,10) ,Header("Set value to 0 to disable this food")]
    public float EatHumanWhen;

    [Range(0,10)]
    public float EatDragonWhen;

    
    [Range(0,10)]
    public float EatPlantWhen;


    [Range(0,10)]
    public float EatCrystalWhen;

}
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FoodChoice", order = 1)]
public class FoodChoice : ScriptableObject
{
    public bool Human, Dragon, Plant, Crystal;
}
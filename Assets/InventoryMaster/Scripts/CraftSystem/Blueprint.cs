using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

[System.Serializable]
public class Blueprint
{

    public List<int> ingredients = new List<int>();
    public List<int> amount = new List<int>();
    [FormerlySerializedAs("finalItem")] public ItemInventory finalItemInventory;
    public int amountOfFinalItem;
    public float timeToCraft;

    public Blueprint(List<int> ingredients, int amountOfFinalItem, List<int> amount, ItemInventory itemInventory)
    {
        this.ingredients = ingredients;
        this.amount = amount;
        finalItemInventory = itemInventory;
    }

    public Blueprint() { }

}
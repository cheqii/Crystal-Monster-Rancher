using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleItems : MonoBehaviour
{
    [SerializeField] private KeyCode[] _keyCodes = new KeyCode[999];
    [SerializeField] private int totalSlots;

    void ToggleKeyItems()
    {
        for (int i = 0; i < GetTotalSlot(); i++)
        {
            ItemOnObject itemOnObject = transform.GetChild(0).GetChild(i).GetComponent<ItemOnObject>();
            
            if(itemOnObject.itemInventory.itemType != ItemType.Consumable) return;
            
            // if()
        }
    }
    
    public int GetTotalSlot()
    {
        totalSlots = this.transform.GetChild(0).childCount;
        return totalSlots;
    }
}

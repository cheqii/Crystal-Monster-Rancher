using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleItems : MonoBehaviour
{
    [SerializeField] private KeyCode[] _keyCodes = new KeyCode[999];
    [SerializeField] private int totalSlots;

    private void Awake()
    {
        GetTotalSlot();
        // _keyCodes = new KeyCode[totalSlots];
    }

    private void Update()
    {
        ToggleKeyItems();
    }

    void ToggleKeyItems()
    {
        for (int i = 0; i < GetTotalSlot(); i++)
        {
            ItemOnObject itemOnObject = transform.GetChild(0).GetChild(i).GetComponent<ItemOnObject>();

            if (Input.GetKeyDown(_keyCodes[i]))
            {
                if (itemOnObject.itemInventory == null)
                {
                    Debug.Log("null return");
                    return;
                }

                if (itemOnObject.itemInventory.itemType == ItemType.Consumable)
                {
                    Debug.Log("destroy?");
                    Destroy(itemOnObject.gameObject);
                }
            }
        }
    }
    
    public int GetTotalSlot()
    {
        totalSlots = this.transform.GetChild(0).childCount;
        return totalSlots;
    }
}

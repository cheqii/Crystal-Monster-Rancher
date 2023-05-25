using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayItemsUI : MonoBehaviour
{
    [SerializeField] private GameObject hotbar;
    private ToggleItems toggle;

    private void Start()
    {
        toggle = GetComponent<ToggleItems>();
    }

    private void Update()
    {
        DuplicateItems();
        toggle.ToggleKeyItems();
    }

    void DuplicateItems()
    {
        for (int i = 0; i < hotbar.transform.GetChild(1).childCount; i++)
        {
            Image itemIcon = this.transform.GetChild(0).GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>(); // get component image from "Slot" child
            TextMeshProUGUI itemText = this.transform.GetChild(0).GetChild(i).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>(); // get component text from "Slot" child
            ItemOnObject itemOnObject = this.transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<ItemOnObject>(); // get component itemOnObject from "Slot"
            
            if (hotbar.transform.GetChild(1).GetChild(i).childCount == 0) // if hotbar slot doesn't have an item on a slot
            {
                itemIcon.enabled = false;
                itemText.text = "";
                itemOnObject.itemInventory = default;
            }
            else if (hotbar.transform.GetChild(1).GetChild(i).childCount >= 1) // if hotbar have the items in slots
            {
                ItemOnObject barItems = hotbar.transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<ItemOnObject>(); // get component itemOnObject (item data) from "Items" as a hotbar child
                Image barIcon = hotbar.transform.GetChild(1).GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>(); // get component image from "hotbar" child
                
                itemOnObject.itemInventory = barItems.itemInventory; // set data in mini hotbar inventory into this slot[i]
                itemText.text = "" + itemOnObject.itemInventory.itemValue;
                itemIcon.sprite = barIcon.sprite;
                itemIcon.enabled = true;

                if (itemOnObject.itemInventory.itemType == ItemType.UFPS_Weapon)
                    itemText.text = "";
            }
        }
    }
}

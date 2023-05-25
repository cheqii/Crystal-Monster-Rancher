using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayItemsUI : MonoBehaviour
{
    [SerializeField] private GameObject hotbar;

    private void Update()
    {
        // DisplayItems();
        DuplicateItems();
    }

    void DisplayItems()
    {
        for (int i = 0; i < hotbar.transform.GetChild(1).childCount; i++)
        {
            Image itemIcon = this.transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<Image>();
            if (hotbar.transform.GetChild(1).GetChild(i).childCount == 0)
                itemIcon.enabled = false;
            
            else
            {
                Image barIcon = hotbar.transform.GetChild(1).GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>();
                itemIcon.sprite = barIcon.sprite;
                itemIcon.enabled = true;
            }
        }
    }

    void DuplicateItems()
    {
        for (int i = 0; i < hotbar.transform.GetChild(1).childCount; i++)
        {
            Image itemIcon = this.transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<Image>();
            TextMeshProUGUI itemText = this.transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>();
            ItemOnObject itemOnObject = this.transform.GetChild(0).GetChild(i).GetComponent<ItemOnObject>();
            if (hotbar.transform.GetChild(1).GetChild(i).childCount == 0)
            {
                itemIcon.enabled = false;
                itemOnObject.itemInventory = null;
            }
            else
            {
                ItemOnObject barItems = hotbar.transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<ItemOnObject>();
                Image barIcon = hotbar.transform.GetChild(1).GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>();
                itemOnObject.itemInventory = barItems.itemInventory;
                itemText.text = "" + itemOnObject.itemInventory.itemValue;
                itemIcon.sprite = barIcon.sprite;
                itemIcon.enabled = true;
            }
        }
    }
}

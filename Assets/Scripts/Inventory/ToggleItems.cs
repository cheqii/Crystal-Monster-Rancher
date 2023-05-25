using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleItems : MonoBehaviour
{
    [SerializeField] private Inventory vent;
    [SerializeField] private GameObject hotbar;
    [SerializeField] private KeyCode[] _keyCodes = new KeyCode[999];
    private int totalSlots;
    private ItemOnObject inv;
    
    private void Awake()
    {
        GetTotalSlot();
        // _keyCodes = new KeyCode[totalSlots];
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        // ToggleKeyItems();
    }

    public void ToggleKeyItems()
    {
        // inv = FindObjectOfType<ItemOnObject>().GetComponent<ItemOnObject>().transform.parent.parent.parent.tag;
        // inv = FindObjectOfType<ItemOnObject>().GetComponent<ItemOnObject>();
        for (int i = 0; i < totalSlots; i++)
        {
            ItemOnObject itemOnObject = transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<ItemOnObject>();
            // ItemInventory items 
            // _inventory.transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<ItemOnObject>();

            if (hotbar.transform.GetChild(1).GetChild(i).childCount == 0) return;
            if (Input.GetKeyDown(_keyCodes[i]))
            {
                if (itemOnObject.itemInventory.itemType == ItemType.Consumable)
                {
                    ItemOnObject inv = hotbar.transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<ItemOnObject>();
                    ItemOnObject bar = hotbar.transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<ItemOnObject>();

                    if(bar.itemInventory.itemID != itemOnObject.itemInventory.itemID) return;
                    Destroy(bar.gameObject);
                    Destroy(inv.gameObject);
                    // vent.updateItemList();
                    // vent.deleteItemFromInventory();
                    // if(inv.itemInventory.itemID != bar.itemInventory.itemID 
                    //    && inv.itemInventory.itemID != itemOnObject.itemInventory.itemID) return;
                    // if (inv.transform.parent.parent)
                    // {
                    //     Destroy(inv.gameObject);
                    //     vent.updateItemList();
                    // }
                    
                    Debug.Log("Destroy : " + itemOnObject.itemInventory.itemName);

                    // set items data in own bar to default
                    itemOnObject.itemInventory = new ItemInventory();
                    itemOnObject.transform.GetChild(0).GetComponent<Image>().enabled = false;
                }
            }
            else
            {
                Debug.Log("Wrong key you have to press this" + _keyCodes[i]);
            }
        }
        
        // for (int x = 0; x < _inventory.transform.GetChild(1).GetChild(x).childCount; x++)
        // {
        //     // hotbar.transform.GetChild(1).GetChild(i).childCount
        //     for (int i = 0; i < totalSlots; i++)
        //     {
        //         ItemOnObject itemOnObject = transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<ItemOnObject>();
        //         ItemOnObject inv = _inventory.transform.GetChild(1).GetChild(x).GetChild(0).GetComponent<ItemOnObject>();
        //
        //         if (hotbar.transform.GetChild(1).GetChild(i).childCount == 0) return;
        //         if (Input.GetKeyDown(_keyCodes[i]))
        //         {
        //             if (itemOnObject.itemInventory.itemType == ItemType.Consumable)
        //             {
        //                 ItemOnObject bar = hotbar.transform.GetChild(1).GetChild(i).GetChild(0)
        //                     .GetComponent<ItemOnObject>();
        //
        //                 if(bar.itemInventory.itemID != itemOnObject.itemInventory.itemID) return;
        //                 Destroy(bar.gameObject);
        //                 if(inv.itemInventory.itemID != bar.itemInventory.itemID 
        //                    && inv.itemInventory.itemID != itemOnObject.itemInventory.itemID) return;
        //                 Destroy(inv.gameObject);
        //                 
        //                 Debug.Log("Destroy : " + itemOnObject.itemInventory.itemName);
        //
        //                 // set items data in own bar to default
        //                 itemOnObject.itemInventory = new ItemInventory();
        //                 itemOnObject.transform.GetChild(0).GetComponent<Image>().enabled = false;
        //             }
        //         }
        //         else
        //         {
        //             Debug.Log("Wrong key you have to press this" + _keyCodes[i]);
        //         }
        //     }
        //
        // }
    }
    
    public int GetTotalSlot()
    {
        totalSlots = this.transform.GetChild(0).childCount;
        return totalSlots;
    }
}

using System;
using UnityEngine;
using UnityEngine.UI;

public class ToggleItems : MonoBehaviour
{
    [SerializeField] private Inventory vent;
    [SerializeField] private GameObject hotbar;
    [SerializeField] private KeyCode[] _keyCodes = new KeyCode[999];

    public KeyCode[] KeyCodes
    {
        get => _keyCodes;
        set => _keyCodes = value;
    }
    
    private int totalSlots;

    [Header("Select & Deselect Slot Color")]
    [SerializeField] private Color selectSlotColor;
    [SerializeField] private Color deselectSlotColor;

    private bool selected;
    private int selectedSlot = -1;
    
    private void Awake()
    {
        GetTotalSlot();
    }

    public void ToggleKeyItems()
    {
        for (int i = 0; i < totalSlots; i++)
        {
            ItemOnObject itemOnObject = transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<ItemOnObject>();
            UseWeapons weapons = transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<UseWeapons>();
            
            if (Input.GetKeyDown(_keyCodes[i]))
            {
                if(itemOnObject.itemInventory == null) return; // if slot have nothing then return
                DeSelectItemsIconSize(75, i);
                SelectItemsIconSize(135, i);
                if (itemOnObject.itemInventory.itemType == ItemType.Consumable)
                {
                    ItemOnObject inv = vent.transform.GetChild(1).GetChild(GetMainTotalSlot()).GetChild(0).GetComponent<ItemOnObject>();
                    ItemOnObject bar = hotbar.transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<ItemOnObject>();
                    
                    if(bar.itemInventory.itemID != itemOnObject.itemInventory.itemID) return;
                    Destroy(bar.gameObject);
                    Destroy(inv.gameObject); // this line doesn't destroy items by id in inventory but destroy by slot itemOnObject : bug
                    vent.updateItemList();

                    Debug.Log("Destroy : " + itemOnObject.itemInventory.itemName);

                    // set items data in own bar to default
                    itemOnObject.itemInventory = new ItemInventory();
                    itemOnObject.transform.GetChild(0).GetComponent<Image>().enabled = false;
                }
                else if (itemOnObject.itemInventory.itemType == ItemType.UFPS_Weapon)
                {
                    // DeSelectItemsIconSize(75, i);
                    weapons.ActivateGun();
                    // DeSelectItemsIconSize(75, i);
                }
            }
        }
    }
    
    public int GetTotalSlot()
    {
        totalSlots = this.transform.GetChild(0).childCount;
        return totalSlots;
    }

    int GetMainTotalSlot()
    {
        for (int i = 0; i < vent.transform.GetChild(1).GetChild(i).childCount; i++)
        {
            if(vent.gameObject.activeSelf) 
                return vent.transform.GetChild(1).GetChild(i).childCount;
        }

        return 0;
    }

    public void SelectItemsIconSize(int iconSize, int id)
    {
        Debug.Log("Update Icon Size When Select Items");

        ItemOnObject itemOnObject = transform.GetChild(0).GetChild(id).GetChild(0).GetComponent<ItemOnObject>();
        RectTransform slot = transform.GetChild(0).GetChild(id).GetChild(0).GetChild(0).GetComponent<RectTransform>();
            
        if(itemOnObject.itemInventory == null) return;
        if (transform.GetChild(0).GetChild(id).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta ==
            new Vector2(iconSize, iconSize)) return;
        slot.sizeDelta = new Vector2(iconSize, iconSize);
        Debug.Log("Hello?");
        
    }

    public void DeSelectItemsIconSize(int iconSize, int id)
    {
        Debug.Log("Update Icon Size When Deselect Items");
        
        for (int i = 0; i < totalSlots; i++)
        {
            ItemOnObject itemOnObject = transform.GetChild(0).GetChild(id).GetChild(0).GetComponent<ItemOnObject>();
            RectTransform slot = transform.GetChild(0).GetChild(i).GetChild(0).GetChild(0).GetComponent<RectTransform>();
            
            if(itemOnObject.itemInventory == null) return;
            if (transform.GetChild(0).GetChild(id).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta ==
                new Vector2(iconSize, iconSize)) return;
            slot.sizeDelta = new Vector2(iconSize, iconSize);
            Debug.Log("Why here ??");
        }
    }
}

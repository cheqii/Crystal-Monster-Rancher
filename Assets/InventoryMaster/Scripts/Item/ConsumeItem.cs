using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Serialization;

public class ConsumeItem : MonoBehaviour, IPointerDownHandler
{
    [FormerlySerializedAs("item")] public ItemInventory itemInventory;
    private static Tooltip tooltip;
    public ItemType[] itemTypeOfSlot;
    public static EquipmentSystem eS;
    public GameObject duplication;
    public static GameObject mainInventory;

    void Start()
    {
        itemInventory = GetComponent<ItemOnObject>().itemInventory;
        if (GameObject.FindGameObjectWithTag("Tooltip") != null)
            tooltip = GameObject.FindGameObjectWithTag("Tooltip").GetComponent<Tooltip>();
        if (GameObject.FindGameObjectWithTag("EquipmentSystem") != null)
            eS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().characterSystem.GetComponent<EquipmentSystem>();

        if (GameObject.FindGameObjectWithTag("MainInventory") != null)
            mainInventory = GameObject.FindGameObjectWithTag("MainInventory");
    }

    public void OnPointerDown(PointerEventData data)
    {
        if (this.gameObject.transform.parent.parent.parent.GetComponent<EquipmentSystem>() == null)
        {
            bool gearable = false;
            Inventory inventory = transform.parent.parent.parent.GetComponent<Inventory>();

            if (eS != null)
                itemTypeOfSlot = eS.itemTypeOfSlots;

            if (data.button == PointerEventData.InputButton.Right)
            {
                //item from craft system to inventory
                if (transform.parent.GetComponent<CraftResultSlot>() != null)
                {
                    bool check = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>().checkIfItemAllreadyExist(itemInventory.itemID, itemInventory.itemValue);

                    if (!check)
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>().addItemToInventory(itemInventory.itemID, itemInventory.itemValue);
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>().stackableSettings();
                    }
                    CraftSystem cS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().craftSystem.GetComponent<CraftSystem>();
                    cS.deleteItems(itemInventory);
                    CraftResultSlot result = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().craftSystem.transform.GetChild(3).GetComponent<CraftResultSlot>();
                    result.temp = 0;
                    tooltip.deactivateTooltip();
                    gearable = true;
                    GameObject.FindGameObjectWithTag("MainInventory").GetComponent<Inventory>().updateItemList();
                }
                else
                {
                    bool stop = false;
                    if (eS != null)
                    {
                        for (int i = 0; i < eS.slotsInTotal; i++)
                        {
                            if (itemTypeOfSlot[i].Equals(itemInventory.itemType))
                            {
                                if (eS.transform.GetChild(1).GetChild(i).childCount == 0)
                                {
                                    stop = true;
                                    if (eS.transform.GetChild(1).GetChild(i).parent.parent.GetComponent<EquipmentSystem>() != null && this.gameObject.transform.parent.parent.parent.GetComponent<EquipmentSystem>() != null) { }
                                    else                                    
                                        inventory.EquiptItem(itemInventory);
                                    
                                    this.gameObject.transform.SetParent(eS.transform.GetChild(1).GetChild(i));
                                    this.gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
                                    eS.gameObject.GetComponent<Inventory>().updateItemList();
                                    inventory.updateItemList();
                                    gearable = true;
                                    if (duplication != null)
                                        Destroy(duplication.gameObject);
                                    break;
                                }
                            }
                        }


                        if (!stop)
                        {
                            for (int i = 0; i < eS.slotsInTotal; i++)
                            {
                                if (itemTypeOfSlot[i].Equals(itemInventory.itemType))
                                {
                                    if (eS.transform.GetChild(1).GetChild(i).childCount != 0)
                                    {
                                        GameObject otherItemFromCharacterSystem = eS.transform.GetChild(1).GetChild(i).GetChild(0).gameObject;
                                        ItemInventory otherSlotItemInventory = otherItemFromCharacterSystem.GetComponent<ItemOnObject>().itemInventory;
                                        if (itemInventory.itemType == ItemType.UFPS_Weapon)
                                        {
                                            inventory.UnEquipItem1(otherItemFromCharacterSystem.GetComponent<ItemOnObject>().itemInventory);
                                            inventory.EquiptItem(itemInventory);
                                        }
                                        else
                                        {
                                            inventory.EquiptItem(itemInventory);
                                            if (itemInventory.itemType != ItemType.Backpack)
                                                inventory.UnEquipItem1(otherItemFromCharacterSystem.GetComponent<ItemOnObject>().itemInventory);
                                        }
                                        if (this == null)
                                        {
                                            GameObject dropItem = (GameObject)Instantiate(otherSlotItemInventory.itemModel);
                                            dropItem.AddComponent<PickUpItem>();
                                            dropItem.GetComponent<PickUpItem>().itemInventory = otherSlotItemInventory;
                                            dropItem.transform.localPosition = GameObject.FindGameObjectWithTag("Player").transform.localPosition;
                                            inventory.OnUpdateItemList();
                                        }
                                        else
                                        {
                                            otherItemFromCharacterSystem.transform.SetParent(this.transform.parent);
                                            otherItemFromCharacterSystem.GetComponent<RectTransform>().localPosition = Vector3.zero;
                                            if (this.gameObject.transform.parent.parent.parent.GetComponent<Hotbar>() != null)
                                                createDuplication(otherItemFromCharacterSystem);

                                            this.gameObject.transform.SetParent(eS.transform.GetChild(1).GetChild(i));
                                            this.gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
                                        }                                        
                                        
                                        gearable = true;                                        
                                        if (duplication != null)
                                            Destroy(duplication.gameObject);
                                        eS.gameObject.GetComponent<Inventory>().updateItemList();
                                        inventory.OnUpdateItemList();
                                        break;
                                    }
                                }
                            }
                        }

                    }

                }
                if (!gearable && itemInventory.itemType != ItemType.UFPS_Ammo && itemInventory.itemType != ItemType.UFPS_Grenade)
                {

                    ItemInventory itemInventoryFromDup = null;
                    if (duplication != null)
                        itemInventoryFromDup = duplication.GetComponent<ItemOnObject>().itemInventory;
                    
                    inventory.ConsumeItem(itemInventory);
                    
                    itemInventory.itemValue--;
                    if (itemInventoryFromDup != null)
                    {
                        duplication.GetComponent<ItemOnObject>().itemInventory.itemValue--;
                        if (itemInventoryFromDup.itemValue <= 0)
                        {
                            if (tooltip != null)
                                tooltip.deactivateTooltip();
                            inventory.deleteItemFromInventory(itemInventory);
                            Destroy(duplication.gameObject); 
                        }
                    }
                    if (itemInventory.itemValue <= 0)
                    {
                        if (tooltip != null)
                            tooltip.deactivateTooltip();
                        inventory.deleteItemFromInventory(itemInventory);
                        Destroy(this.gameObject);                        
                    }

                }
                
            }
            

        }
    }    

    public void consumeIt()
    {
        Inventory inventory = transform.parent.parent.parent.GetComponent<Inventory>();

        bool gearable = false;

        if (GameObject.FindGameObjectWithTag("EquipmentSystem") != null)
            eS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().characterSystem.GetComponent<EquipmentSystem>();

        if (eS != null)
            itemTypeOfSlot = eS.itemTypeOfSlots;

        ItemInventory itemInventoryFromDup = null;
        if (duplication != null)
            itemInventoryFromDup = duplication.GetComponent<ItemOnObject>().itemInventory;       

        bool stop = false;
        if (eS != null)
        {
            
            for (int i = 0; i < eS.slotsInTotal; i++)
            {
                if (itemTypeOfSlot[i].Equals(itemInventory.itemType))
                {
                    if (eS.transform.GetChild(1).GetChild(i).childCount == 0)
                    {
                        stop = true;
                        this.gameObject.transform.SetParent(eS.transform.GetChild(1).GetChild(i));
                        this.gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
                        eS.gameObject.GetComponent<Inventory>().updateItemList();
                        inventory.updateItemList();
                        inventory.EquiptItem(itemInventory);
                        gearable = true;
                        if (duplication != null)
                            Destroy(duplication.gameObject);
                        break;
                    }
                }
            }

            if (!stop)
            {
                for (int i = 0; i < eS.slotsInTotal; i++)
                {
                    if (itemTypeOfSlot[i].Equals(itemInventory.itemType))
                    {
                        if (eS.transform.GetChild(1).GetChild(i).childCount != 0)
                        {
                            GameObject otherItemFromCharacterSystem = eS.transform.GetChild(1).GetChild(i).GetChild(0).gameObject;
                            ItemInventory otherSlotItemInventory = otherItemFromCharacterSystem.GetComponent<ItemOnObject>().itemInventory;
                            if (itemInventory.itemType == ItemType.UFPS_Weapon)
                            {
                                inventory.UnEquipItem1(otherItemFromCharacterSystem.GetComponent<ItemOnObject>().itemInventory);
                                inventory.EquiptItem(itemInventory);
                            }
                            else
                            {
                                inventory.EquiptItem(itemInventory);
                                if (itemInventory.itemType != ItemType.Backpack)
                                    inventory.UnEquipItem1(otherItemFromCharacterSystem.GetComponent<ItemOnObject>().itemInventory);
                            }
                            if (this == null)
                            {
                                GameObject dropItem = (GameObject)Instantiate(otherSlotItemInventory.itemModel);
                                dropItem.AddComponent<PickUpItem>();
                                dropItem.GetComponent<PickUpItem>().itemInventory = otherSlotItemInventory;
                                dropItem.transform.localPosition = GameObject.FindGameObjectWithTag("Player").transform.localPosition;
                                inventory.OnUpdateItemList();
                            }
                            else
                            {
                                otherItemFromCharacterSystem.transform.SetParent(this.transform.parent);
                                otherItemFromCharacterSystem.GetComponent<RectTransform>().localPosition = Vector3.zero;
                                if (this.gameObject.transform.parent.parent.parent.GetComponent<Hotbar>() != null)
                                    createDuplication(otherItemFromCharacterSystem);

                                this.gameObject.transform.SetParent(eS.transform.GetChild(1).GetChild(i));
                                this.gameObject.GetComponent<RectTransform>().localPosition = Vector3.zero;
                            }

                            gearable = true;
                            if (duplication != null)
                                Destroy(duplication.gameObject);
                            eS.gameObject.GetComponent<Inventory>().updateItemList();
                            inventory.OnUpdateItemList();
                            break;                           
                        }
                    }
                }
            }


        }
        if (!gearable && itemInventory.itemType != ItemType.UFPS_Ammo && itemInventory.itemType != ItemType.UFPS_Grenade)
        {

            if (duplication != null)
                itemInventoryFromDup = duplication.GetComponent<ItemOnObject>().itemInventory;

            inventory.ConsumeItem(itemInventory);


            itemInventory.itemValue--;
            if (itemInventoryFromDup != null)
            {
                duplication.GetComponent<ItemOnObject>().itemInventory.itemValue--;
                if (itemInventoryFromDup.itemValue <= 0)
                {
                    if (tooltip != null)
                        tooltip.deactivateTooltip();
                    inventory.deleteItemFromInventory(itemInventory);
                    Destroy(duplication.gameObject);

                }
            }
            if (itemInventory.itemValue <= 0)
            {
                if (tooltip != null)
                    tooltip.deactivateTooltip();
                inventory.deleteItemFromInventory(itemInventory);
                Destroy(this.gameObject); 
            }

        }        
    }

    public void createDuplication(GameObject Item)
    {
        ItemInventory itemInventory = Item.GetComponent<ItemOnObject>().itemInventory;
        GameObject dup = mainInventory.GetComponent<Inventory>().addItemToInventory(itemInventory.itemID, itemInventory.itemValue);
        Item.GetComponent<ConsumeItem>().duplication = dup;
        dup.GetComponent<ConsumeItem>().duplication = Item;
    }
}

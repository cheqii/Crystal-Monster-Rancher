using UnityEngine;
using UnityEngine.UI;

public class ToggleItems : MonoBehaviour
{
    [SerializeField] private Inventory vent;
    [SerializeField] private GameObject hotbar;
    [SerializeField] private KeyCode[] _keyCodes = new KeyCode[999];
    private int totalSlots;
    // private ItemOnObject inv;
    
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
                if (itemOnObject.itemInventory.itemType == ItemType.Consumable)
                {
                    ItemOnObject inv = vent.transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<ItemOnObject>();
                    ItemOnObject bar = hotbar.transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<ItemOnObject>();
                    
                    if(bar.itemInventory.itemID != itemOnObject.itemInventory.itemID) return;
                    Destroy(bar.gameObject);
                    Destroy(inv.gameObject);
                    vent.updateItemList();

                    Debug.Log("Destroy : " + itemOnObject.itemInventory.itemName);

                    // set items data in own bar to default
                    itemOnObject.itemInventory = new ItemInventory();
                    itemOnObject.transform.GetChild(0).GetComponent<Image>().enabled = false;
                }
                else if (itemOnObject.itemInventory.itemType == ItemType.UFPS_Weapon)
                {
                    weapons.ActivateGun();
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

using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class ItemOnObject : MonoBehaviour                   //Saves the Item in the slot
{
    [FormerlySerializedAs("item")] public ItemInventory itemInventory;                                       //Item 
    private Text text; //text for the itemValue

    public Text _Text
    {
        get => text;
        set => text = value;
    }
    
    private Image image;

    void Update()
    {
        if(this.transform.parent.parent.parent.tag == "OwnHotbar") return;
        text.text = "" + itemInventory.itemValue; //sets the itemValue         
        image.sprite = itemInventory.itemIcon;
        GetComponent<ConsumeItem>().itemInventory = itemInventory;
    }

    void Start()
    {
        image = transform.GetChild(0).GetComponent<Image>();
        transform.GetChild(0).GetComponent<Image>().sprite = itemInventory.itemIcon;                 //set the sprite of the Item 
        text = transform.GetChild(1).GetComponent<Text>();                                //get the text(itemValue GameObject) of the item
    }
}

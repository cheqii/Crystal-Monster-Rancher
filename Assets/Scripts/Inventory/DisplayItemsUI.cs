using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayItemsUI : MonoBehaviour
{
    [SerializeField] private GameObject hotbar;

    private void Update()
    {
        DisplayItems();
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
}

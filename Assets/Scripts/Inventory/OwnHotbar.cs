using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnHotbar : MonoBehaviour
{
    public bool barActive;

    private void Start()
    {
        barActive = true;
    }

    public void DisableHotbarSlot()
    {
        if(!barActive)
        {
            Debug.Log("Hotbar is not active");
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Hotbar is active");
            gameObject.SetActive(true);
        }
    }
}

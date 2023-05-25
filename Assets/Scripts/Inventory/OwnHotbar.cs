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
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}

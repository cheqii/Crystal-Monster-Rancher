using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnHotbar : MonoBehaviour
{
    // [SerializeField] private GameObject inventory;
    // [SerializeField] private GameObject bigHotbarSlot;

    [SerializeField] private GameObject inventory;
    public bool barActive;
    private InventoryInputManager inputManagerDatabase;

    private void Start()
    {
        barActive = true;
        // inv = GetComponent<Inventory>();
        if(inputManagerDatabase == null)
            inputManagerDatabase = (InventoryInputManager)Resources.Load("InputManager");
    }

    // Update is called once per frame
    void Update()
    {
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAnimalStatus : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<Billboard>(true) != null)
        {
            var status = other.GetComponentsInChildren<Billboard>(true);

            foreach (var i in status)
            {
                i.gameObject.SetActive(true);
            }
            
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInChildren<Billboard>() != null)
        {
            var status = other.GetComponentsInChildren<Billboard>();

            foreach (var i in status)
            {
                i.gameObject.SetActive(false);
            }
            
        }
    }
}

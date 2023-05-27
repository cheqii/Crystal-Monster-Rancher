using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CrystalMachine : MonoBehaviour
{
    [SerializeField] private Transform textPostition;
    [SerializeField] private DynamicTextData _textData;
    
    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Crystal")
        {
            DynamicTextManager.CreateText(
                textPostition.position,
                Random.Range(10,20)  +" $",
                _textData);
            Destroy(col);
        }
            
    }
}

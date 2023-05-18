using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alleles : MonoBehaviour
{
    [SerializeField] private int Allele_1_Streght;
    [SerializeField] private int Allele_2_Streght;
    [SerializeField] private bool isPureBreed;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        IsPureBlood();
    }
    

    private void IsPureBlood()
    {
        if (isPureBreed == true)
        {
            Allele_2_Streght = Allele_1_Streght;
        }
    }

    public virtual void SetEffects()
    {
        Debug.Log("not override yet (Alleles.cs)");
    }
}

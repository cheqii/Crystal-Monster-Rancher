using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alleles : MonoBehaviour
{
    public enum AllelesType
    {
        Color,
        Size
    }


    [Header("Genotype")]
    public int Allele_1_Streght;
    public int Allele_2_Streght;
    public bool isPureBreed;

    [Header("Phenotype")]
    public AllelesType Type;
    
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

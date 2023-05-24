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
    [Range(0.0f, 10.0f)]
    public float Allele_1_Dominant;
    [Range(0.0f, 10.0f)]
    public float Allele_2_Dominant;
    public bool isPureBreed;

    [Header("Phenotype")]
    public AllelesType Type;
    
    // Start is called before the first frame update
    protected void Setup()
    {
        IsPureBlood();
    }
    

    private void IsPureBlood()
    {
        if (isPureBreed == true)
        {
            Allele_2_Dominant = Allele_1_Dominant;
        }
    }

    public virtual void SetEffects()
    {
        Debug.Log("not override yet (Alleles.cs)");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeAlleles : Alleles
{
    public float a1_size;
    public float a2_size;



    public void Setup()
    {
        base.Setup();

        if (isPureBreed == true)
        {
            a2_size = a1_size;
        }
    }
    
    
    
    public override void SetEffects()
    {
        Setup();
        float newMaxSize;
        
        var creature = GetComponent<Creature>();
        
        if (Allele_1_Dominant > Allele_2_Dominant)
        {
            creature.SetSize(a1_size);
        }
        else
        {
            creature.SetSize(a2_size);
        }

        //more dominant gap = more dominant on single allele
        // divide 10 to get 0.0 to 1.0
        var dominantGap = Mathf.Abs(Allele_1_Dominant - Allele_2_Dominant)/10;

        float averageSize = (a1_size + a2_size) / 2;
        
        
        newMaxSize = Mathf.Lerp(creature.SizeLimit, averageSize,1 - dominantGap);
        
        creature.SetSize(newMaxSize);

    }
}

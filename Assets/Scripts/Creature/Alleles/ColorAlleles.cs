using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorAlleles : Alleles
{
    public Color a1_color;
    public Color a2_color;
    
    
    
    public void Setup()
    {
        base.Setup();

        if (isPureBreed == true)
        {
            a2_color = a1_color;
        }
    }
    
    
    
    public override void SetEffects()
    {
        Setup();
        
        Color newColor;
        
        var creature = GetComponent<Creature>();
        
        if (Allele_1_Dominant > Allele_2_Dominant)
        {
            creature.SetColor(a1_color);
        }
        else
        {
            creature.SetColor(a2_color);
        }

        //more dominant gap = more dominant on single allele
        // divide 10 to get 0.0 to 1.0
        var dominantGap = Mathf.Abs(Allele_1_Dominant - Allele_2_Dominant)/10;

        Color averageColor = Color.Lerp(a1_color, a2_color, 0.5f);
        
        
        newColor = Color.Lerp(creature.Color, averageColor,1 - dominantGap);
        
        creature.SetColor(newColor);

    }
    
}

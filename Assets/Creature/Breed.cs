using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breed : MonoBehaviour
{
    [SerializeField]
    private Creature creatureA;
    [SerializeField]
    private Creature creatureB;
    
    public static Breed Instance { get; private set; }
    
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
    
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
    
    public void Test()
    {
        Breeding(creatureA, creatureB, transform);
    }

    public void Breeding(Creature a, Creature b, Transform pos)
    {
        //return when not in condition
        if (a.GetGenetic().Length <= 0 || b.GetGenetic().Length <= 0) return;
        if (a.GetSex() == b.GetSex()) return;
        
        
        //Instantiate child
        var child = Instantiate(a.gameObject, pos.position, Quaternion.identity);
        for (int i = 0; i < a.GetGenetic().Length; i++)
        {
            
            Alleles a_Alleles = a.GetGenetic()[i];
            Alleles b_Alleles = b.GetGenetic()[i];


            Vector2 assortment = Assortment(a_Alleles,b_Alleles);


            //check what is alleles type
            
            //ColorAlleles
            if (a_Alleles.Type == Alleles.AllelesType.Color)
            {
                var a_ColorAlleles = a_Alleles.gameObject.GetComponent<ColorAlleles>();
                var b_ColorAlleles = b_Alleles.gameObject.GetComponent<ColorAlleles>();
                var childColorAlleles = child.GetComponent<ColorAlleles>();

                SetChildGenetic(childColorAlleles, a_ColorAlleles, b_ColorAlleles, assortment);
            }
            //Size Alleles
            else if (a_Alleles.Type == Alleles.AllelesType.Size)
            {
                var a_SizeAlleles = a_Alleles.gameObject.GetComponent<SizeAlleles>();
                var b_SizeAlleles = b_Alleles.gameObject.GetComponent<SizeAlleles>();
                var childSizeAlleles = child.GetComponent<SizeAlleles>();

                SetChildGenetic(childSizeAlleles, a_SizeAlleles, b_SizeAlleles, assortment);

            }

        }
        
        
    }

    public void SetChildGenetic(ColorAlleles childColorAlleles, ColorAlleles a, ColorAlleles b, Vector2 assortment)
    {
        childColorAlleles.isPureBreed = false;
        
        if (assortment.x == 0)
        {
            childColorAlleles.Allele_1_Streght = a.Allele_1_Streght;
            childColorAlleles.a1_color = a.a1_color;
        }
        else if (assortment.x == 1)
        {
            childColorAlleles.Allele_1_Streght = a.Allele_2_Streght;
            childColorAlleles.a1_color = a.a2_color;

        }
        
        if (assortment.y == 0)
        {
            childColorAlleles.Allele_2_Streght = b.Allele_1_Streght;
            childColorAlleles.a2_color = b.a1_color;

        }
        else if (assortment.y == 1)
        {
            childColorAlleles.Allele_2_Streght = b.Allele_2_Streght;
            childColorAlleles.a2_color = b.a2_color;
        }
    }
    
    //overload Size
    public void SetChildGenetic(SizeAlleles childSizeAlleles, SizeAlleles a, SizeAlleles b, Vector2 assortment)
    {
        if (assortment.x == 0)
        {
            childSizeAlleles.Allele_1_Streght = a.Allele_1_Streght;
            childSizeAlleles.a1_size = a.a1_size;
        }
        else if (assortment.x == 1)
        {
            childSizeAlleles.Allele_1_Streght = a.Allele_2_Streght;
            childSizeAlleles.a1_size = a.a2_size;

        }
        
        if (assortment.y == 0)
        {
            childSizeAlleles.Allele_2_Streght = a.Allele_1_Streght;
            childSizeAlleles.a2_size = b.a1_size;

        }
        else if (assortment.x == 1)
        {
            childSizeAlleles.Allele_2_Streght = a.Allele_2_Streght;
            childSizeAlleles.a2_size = b.a2_size;
        }
    }
    
    
    

    
    //Law of independent assortment
    private Vector2 Assortment(Alleles a, Alleles b)
    {

        switch (Random.Range(0,2))
        {
            case 0:
                //a_alleles 1
                    
                if (Random.Range(0, 2) == 0)
                { 
                    //b_alleles 1
                    return new Vector2(0, 0);
                }
                else
                {
                    //b_alleles 2
                    return new Vector2(0, 1);
                }
            
                
            case 1: 
                //a_alleles 2

                if (Random.Range(0, 2) == 0)
                { 
                    //b_alleles 1
                    return new Vector2(1, 0);

                }
                else
                {
                    //b_alleles 2
                    return new Vector2(1, 1);

                }
        }

        return default;
    }
    
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour,IGrowable,IDamagable,IValue
{
    public enum Species
    {
        Dragon,
        Crystal,
        Plant
    }




    [SerializeField] protected Species Specie;
    [SerializeField] protected SexEnum.Sex Sex;
    public SexEnum.Sex GetSex()
    {
        return Sex;
    } 
    [SerializeField] protected  Color Color;
    [SerializeField] protected  float Size;
    [SerializeField] protected  float SleepTIme;
    [SerializeField] protected  bool CanSleep;
    [SerializeField] protected  int Hp;
    
    [SerializeField] protected Alleles[] Genetic;
    public Alleles[] GetGenetic()
    {
        return Genetic;
    } 


    //IGrowable
    public float GrowRate { get; set; }

    public float SizeLimit { get; set; }

    public float EatDelay { get; set; }

    public bool NeedFood { get; set; }

    //IValue
    public int _Value { get; set; }


    // Start is called before the first frame update
    void Awake()
    {
        Genetic = GetComponents<Alleles>();

        foreach (var v in Genetic)
        {
            v.SetEffects();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int amount)
    {
        
    }


    public virtual void SetupAlleles()
    {
        
    }

    public virtual void Attack()
    {
        
    }

    public virtual void Dead()
    {
        
    }

    public virtual void Sleep()
    {
        
    }
    
}

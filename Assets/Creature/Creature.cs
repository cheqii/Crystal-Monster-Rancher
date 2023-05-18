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




    [SerializeField] protected  Species _species;
    [SerializeField] protected  Color color;
    [SerializeField] protected  float size;
    [SerializeField] protected  float sleepTIme;
    [SerializeField] protected  bool canSleep;
    [SerializeField] protected  int hp;
    [SerializeField] protected  Alleles[] genetic;


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
        genetic = GetComponents<Alleles>();
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

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




    [field: SerializeField]
    public Species Specie { get; protected set; }
    
    [field: SerializeField]
    public SexEnum.Sex Sex { get; protected set; }
    
    [field: SerializeField]
    public Color Color { get; protected set; }
    
    [field: SerializeField]
    public  float Size { get; protected set; }
    
    [field: SerializeField]
    public  float SleepTime { get; protected set; }
    
    [field: SerializeField]
    public  bool CanSleep { get; protected set; }
    
    [field: SerializeField]
    public  int Hp { get; protected set; }
    
    [field: SerializeField]
    public Animator _anim{ get; protected set; }
    
    
    [SerializeField] protected Alleles[] Genetic;
    public Alleles[] GetGenetic()
    { return Genetic; } 
    
    public Creature attackTarget;



    //IGrowable
    public float GrowRate { get; set; }

    public float SizeLimit { get; set; }

    public float EatDelay { get; set; }

    public bool NeedFood { get; set; }

    //IValue
    public int _Value { get; set; }
    
    


    // Start is called before the first frame update
    void Start()
    {
        Genetic = GetComponents<Alleles>();

        foreach (var v in Genetic)
        {
            v.SetEffects();
        }

        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public virtual void Radar(Transform target)
    {
        
    }
    
    public virtual void StopWalking(Transform target)
    {
        
    }
    
    public virtual void Damage (int amount)
    {
        Debug.Log(transform.name + " | hurt");
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
    
    public void SetColor(Color _color)
    {
        Color = _color;
    }

    protected void SetAnimationTrigger( string name)
    {
        _anim.SetTrigger(name);
    }
    
    
    protected void SetAnimationBool(AnimationEvent myEvent) 
    {
        //string| = name    int| 1 = true, 0 = false
        _anim.SetBool(myEvent.stringParameter, myEvent.intParameter != 0);
    }
    
  
    
}

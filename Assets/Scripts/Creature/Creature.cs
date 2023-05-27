using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Creature : MonoBehaviour,IGrowable,IDamagable,IValue
{
    public enum Species
    {
        Dragon,
        Crystal,
        Plant
    }

    [SerializeField, Header("Color Material")] public Renderer renderer;

    [field: SerializeField]
    public float MaxSpeed { get; protected set; }


    [field: SerializeField]
    public Species Specie { get; protected set; }
    
    [field: SerializeField]
    public SexEnum.Sex Sex { get; protected set; }
    
    [field: SerializeField]
    public Color Color { get; protected set; }
    
    [field: SerializeField]
    public  float Size { get; protected set; }
    
    

    [field: SerializeField , Header("Sleep")]
    public  int SleepDelay { get; protected set; }
    [field: SerializeField]
    public  int CrystalSleepTime { get; protected set; }

    
    [field: SerializeField]
    public  bool CanSleep { get; protected set; }
    
    [field: SerializeField]
    public  int MaxHp { get; protected set; }
    
    [field: SerializeField]
    public  int Hp { get; protected set; }
    
    [field: SerializeField]
    public  bool IsDead { get; protected set; }
    
    [field: SerializeField]
    public  bool IsDissolve { get; protected set; }
    
    public Animator _anim { get; protected set; }
    
    //flee
    [field: SerializeField ]
    public bool isFlee { get; set; }
    [field: SerializeField ]
    public float fleeDistance { get; set; }
    
    
    [SerializeField] protected Alleles[] Genetic;
    public Alleles[] GetGenetic()
    { return Genetic; } 
    
 
    public Creature attackTarget;

    

    //IGrowable
    public float MaxStomach { get; set; }
    
    [field: SerializeField,Header("Food") , Range(0,10)]
    public float CurrentStomach { get; set; }
    [field: SerializeField , Range(0,10)]
    public float SizeLimit { get; set; }
    
    [field: SerializeField]
    public float FoodIngestDelay { get; set; }

    //for behaviour
    public bool NeedFood { get; set; }

    //IValue
    [field: SerializeField , Range(0,10)]
    public int _Value { get; set; }

    public GameObject dropCrystal;


    private void Awake()
    {
        IsDead = false;
        IsDissolve = false;

        MaxStomach = 10;
    }

    // Start is called before the first frame update
    protected  void OnEnable()
    {
        Genetic = GetComponents<Alleles>();

        foreach (var v in Genetic)
        {
            v.SetEffects();
        }

        _anim = GetComponent<Animator>();

        if (CanSleep)
        {
            StartCoroutine(SleepTimer());
        }

        StartCoroutine(Grow());
    }

    // Update is called once per frame
    protected void Update()
    {
       

        if (Hp <= 0)
        {
            Dead();
        }
        
        //need food
        if (CurrentStomach < MaxStomach/2)
        {
            NeedFood = true;
        }
        else
        {
            NeedFood = false;
        }

        

        transform.localScale = new Vector3(Size, Size, Size);

    }

    
    public virtual void Radar(Transform target)
    {
        
    }
    
    public virtual void StopWalking(Transform target)
    {
        
    }

    public virtual void Flee()
    {
    }

    public virtual void Damage (int amount, GameObject damageDealer)
    {
        Hp -= amount;
    }


    public virtual void SetupAlleles()
    {
        
    }

    public virtual void Attack()
    {
        if(IsDead == true) return;
    }

    public virtual void Dead()
    {
        IsDead = true;
    }
    
    public virtual void BodyDissolve()
    {
        IsDissolve = true;
        Debug.Log("disolve " + IsDissolve);
        ParticleManager.Do.SpawnParticle(TempObject.Instance.CrystalParticle, transform.position, 10,3);
        TempObject.Instance.DestroyDelay(this.gameObject,3);
    }

    public virtual void Sleep()
    {
        
    }
    
    public void SetColor(Color _color)
    {
        Color = _color;
    }
    
    public void SetSize(float _size)
    {
        SizeLimit = _size;
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

    protected virtual IEnumerator SleepTimer()
    {
        var timer = SleepDelay;
        
        while (true)
        {
            if (!Application.isPlaying)   yield break;
    
            
            if (!attackTarget)
            {
                timer--;
            }
                
            if(timer <= 0 )
            {
                Sleep();
            }
                
            yield return new WaitForSeconds(1);
        }
        
    }

    protected virtual IEnumerator Grow()
    {
        var timer = FoodIngestDelay;

        while (true)
        {
            
            
            if (!Application.isPlaying)  yield break;

            timer--;

            //if digest
            if (timer <= 0)
            {
                Instantiate(dropCrystal,transform.position,Quaternion.identity);

                
                timer = FoodIngestDelay;
                
                //if food in Stomach more than half of MaxStomach
                if (Size < SizeLimit && CurrentStomach > MaxStomach * 0.9)
                {
                    //gain 10% size progress
                    Size += SizeLimit / 100;
                    
                    //lose 20% of food
                    CurrentStomach -= MaxStomach / 5;
                }


                
                //lose 1% of food
                CurrentStomach -= MaxStomach / 100;
                
                if(CurrentStomach <= 0)
                {
                    CurrentStomach = 0;
                    Hp -= MaxHp / 100;
                }
                else
                {
                    //heal if have food
                    Hp += MaxHp / 100;

                }
                
                
         


            }
            yield return new WaitForSeconds(1);
        }
    }



}

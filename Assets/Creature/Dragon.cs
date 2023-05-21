using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[ExecuteInEditMode]
public class Dragon : Creature,ICrystallizable,IWander
{
    [SerializeField] private FoodChoice _foodChoice;

    [Range(0f,30f)]
    [SerializeField] private float moveSpeed;
    [SerializeField, Header("Color Renderer")] private GameObject renderer;
    private NavMeshAgent _ai;
    private AttackArea _attackArea;
    
    
    private bool isEatHuman, isEatDragon, isEatPlant, isEatCrystal;
    private IWander _wanderImplementation;


    [field: SerializeField , Header("Wander")]
    public float wanderRadius { get; set; }
    [field: SerializeField,Range(0,100)]
    public float wanderTimer { get; set; }
    [field: SerializeField,Range(0,100)]
    public float wanderDelay { get; set; }
    [field: SerializeField ]
    public bool isWander { get; set; }


    // Start is called before the first frame update
    void OnEnable()
    {
        base.OnEnable();
        
        //set food choices
        isEatHuman = _foodChoice.Human;
        isEatDragon = _foodChoice.Dragon;
        isEatPlant = _foodChoice.Plant;
        isEatCrystal = _foodChoice.Crystal;

        
        _attackArea = GetComponentInChildren<AttackArea>();
        Specie = Species.Dragon;
        _ai = GetComponent<NavMeshAgent>();
        _ai.speed = moveSpeed;
        
        
        GetComponent<Animator>().SetTrigger("Idle");
        
        isWander = true;

        
        wanderTimer = wanderDelay;
    }

    
    void Update()
    {
        base.Update();
        
        //create new mat (fix later)
        Material newMat = new Material(renderer.GetComponent<Renderer>().sharedMaterial);
        newMat.color = Color;
        renderer.GetComponent<Renderer>().sharedMaterial = newMat;
        
        
        _ai.velocity = _ai.desiredVelocity;

        Wander();

   
    }




    //only play animation (damage deal is on deal damage method)
    public override void Attack()
    {
        SetAnimationTrigger("Attack");
    }

    public void RunToTarget(Transform target, string animName)
    {
        if (_anim.GetBool("CanMove") == false) return;
        
        isWander = false;

        _ai.SetDestination(target.transform.position);
        attackTarget = target.GetComponent<Creature>();
        _attackArea.target = target;
        SetAnimationTrigger(animName);
    }
    
    
    
    public override void Radar(Transform target)
    {
        if(NeedFood == false) return;

        _ai.isStopped = false;

        switch (target.transform.tag)
        {
            case "Player" :
                if(isEatHuman == false) return;
                RunToTarget(target,"Run");
                break;
            
            case "Dragon" :
                
                if(isEatDragon == false) return;
                RunToTarget(target,"Run");
                break;
            
            case "Plant" :
                if(isEatPlant == false) return;
                RunToTarget(target,"Walk");
                break;
            
            case "Crystal" :
                if(isEatCrystal == false) return;
                RunToTarget(target,"Walk");
                break;
        }
    }

    //stop when target is out of radar
    public override void StopWalking(Transform target)
    {
        if(attackTarget == null) return;
        
        if (target.gameObject == attackTarget.gameObject)
        {
            Debug.Log(transform.name);
            _ai.velocity = Vector3.zero;
            _ai.isStopped = true;
            SetAnimationTrigger("Idle");


            isWander = true;
        }
    }
    
    
    //use animation event
    public void DealDamage ()
    {
        if (attackTarget != null)
        {
            if (attackTarget.IsDead == true)
            {
                CurrentStomach = MaxStomach;
            }
            attackTarget.Damage(20);

        }
    }
    
    private void Fly()
    {
        
    }
    
    private void Eat()
    {
        
    }

    public void SetIsWander(bool _bool)
    {
        isWander = _bool;
    }

    
    public override void Sleep()
    {
        Crystallize();
    }

    public void Crystallize()
    {
        var crystal =  Instantiate(TempObject.Instance.SoulCrystal, transform.position, Quaternion.identity);
        var crystalParticle =  Instantiate(TempObject.Instance.CrystalParticle, transform.position, Quaternion.identity);
        crystalParticle.transform.SetParent(crystal.transform);

        //set crystal size
        crystal.transform.localScale = new Vector3(Size * 10, Size * 10, Size * 10);
        
        var soulCrystal = crystal.GetComponent<SoulCrystal>();
        
        TempObject.Instance.DestroyDelay(crystalParticle,2);

        soulCrystal.CrystalSetup(Sex,Color,Size,CrystalSleepTime);
        
        this.transform.SetParent(crystal.transform);
        this.gameObject.SetActive(false);
    }


    public void Wander()
    {
        //if target missing go wander
        if (attackTarget == null)
        {
            SetIsWander(true);
        }
        
        
        if(isWander == false) return;
        
        //wander
        wanderTimer += Time.deltaTime;

        if (wanderTimer >= wanderDelay)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            _ai.SetDestination(newPos);
            wanderTimer = 0;
        }
        else if(_ai.remainingDistance > _ai.stoppingDistance)
        {
            _anim.SetBool("CanMove",true);
            SetAnimationTrigger("Walk");
            Debug.Log("runn");
        }
        else if(_ai.remainingDistance <= _ai.stoppingDistance)
        {
            _anim.SetBool("CanMove",false);
            SetAnimationTrigger("Idle");
            Debug.Log("Idle");

        }


  
    }
    
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
        Vector3 randDirection = Random.insideUnitSphere * dist;
 
        randDirection += origin;
 
        NavMeshHit navHit;
 
        NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
 
        return navHit.position;
    }
    
}

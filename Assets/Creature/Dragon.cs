using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[ExecuteInEditMode]
public class Dragon : Creature,ICrystallizable
{
    [SerializeField] private FoodChoice _foodChoice;

    [Range(0f,30f)]
    [SerializeField] private float moveSpeed;
    [SerializeField, Header("Color Renderer")] private GameObject renderer;
    private NavMeshAgent _ai;
    private AttackArea _attackArea;
    
    
    private bool isEatHuman, isEatDragon, isEatPlant, isEatCrystal;

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

        
    }

    
    void Update()
    {
        base.Update();
        
        //create new mat (fix later)
        Material newMat = new Material(renderer.GetComponent<Renderer>().sharedMaterial);
        newMat.color = Color;
        renderer.GetComponent<Renderer>().sharedMaterial = newMat;
        
        
        _ai.velocity = _ai.desiredVelocity;
    }




    //only play animation (damage deal is on deal damage method)
    public override void Attack()
    {
        SetAnimationTrigger("Attack");
    }

    public void RunToTarget(Transform target, string animName)
    {
        if (_anim.GetBool("CanMove") == false) return;
        
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
    
    public override void Sleep()
    {
        Crystallize();
    }

    public void Crystallize()
    {
        var crystal =  Instantiate(TempObject.Instance.SoulCrystal, transform.position, Quaternion.identity);
        var crystalParticle =  Instantiate(TempObject.Instance.CrystalParticle, transform.position, Quaternion.identity);
        crystalParticle.transform.SetParent(crystal.transform);
        
        var soulCrystal = crystal.GetComponent<SoulCrystal>();
        
        TempObject.Instance.DestroyDelay(crystalParticle,2);

        soulCrystal.CrystalSetup(Sex,Color,Size,CrystalSleepTime);
        
        this.transform.SetParent(crystal.transform);
        this.gameObject.SetActive(false);
    }

    
 
}

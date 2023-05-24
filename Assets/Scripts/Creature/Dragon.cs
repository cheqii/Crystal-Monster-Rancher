using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


[ExecuteInEditMode]
public class Dragon : Creature,ICrystallizable,IWander
{
    [SerializeField] private FoodChoice _foodChoice;

    [Range(0f,30f)]
    [SerializeField] private float moveSpeed;
    [SerializeField, Header("Color Material")] private Renderer renderer;
    private NavMeshAgent _ai;
    private AttackArea _attackArea;
    private IWander _wanderImplementation;


    [field: SerializeField , Header("Wander")]
    public float wanderRadius { get; set; }
    [field: SerializeField,Range(0,100)]
    public float wanderTimer { get; set; }
    [field: SerializeField,Range(0,100)]
    public float wanderDelay { get; set; }
    [field: SerializeField ]
    public bool isWander { get; set; }

    [Header("Attack Damage")]
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;


    private bool isEatDragon, isEatPlant, isEatHuman, isEatCrystal;



    // Start is called before the first frame update
    void OnEnable()
    {
        base.OnEnable();
        

        _attackArea = GetComponentInChildren<AttackArea>();
        Specie = Species.Dragon;
        _ai = GetComponent<NavMeshAgent>();
        _ai.speed = moveSpeed;
        
        
        GetComponent<Animator>().SetTrigger("Idle");
        
        isWander = true;

        
        wanderTimer = wanderDelay;
        
    }

    private void Start()
    {
        if (!Application.isPlaying)  return;

        renderer.material = new Material(renderer.material);
    }


    void Update()
    {
        
   
        //if not playing dont run
        if (!Application.isPlaying)  return;
        
        
        //change color
        if (renderer.material.HasProperty("_ToColor"))
        {
            renderer.material.SetColor("_ToColor",Color);
        }
        
        base.Update();

        _ai.velocity = _ai.desiredVelocity;
        
        //hungry kinesis clamp
        _ai.speed = Mathf.Clamp(_ai.speed, 1, MaxSpeed);

        Flee();

        //if dead dont move
        if (IsDead)
        {
            _ai.velocity = Vector3.zero;
        }
        
   

        
        FoodChoice();

        Wander();
        
        
        //animation speed
        float walkSpeed_anim = (_ai.speed/2);
        _anim.SetFloat("WalkSpeed", walkSpeed_anim);
    }

    public override void Flee()
    {
        //flee if hp under20%
        isFlee = Hp <= MaxHp / 5;

        if (isFlee && _ai.isStopped != true)
        {
            float distance = Vector3.Distance(transform.GetChild(0).position, attackTarget.transform.GetChild(0).position);

            if (distance < fleeDistance)
            {
                _ai.speed = MaxSpeed * 0.8f;
                Vector3 dirToPlayer = transform.GetChild(0).position - attackTarget.transform.GetChild(0).position * 2;
                Vector3 newPos = transform.GetChild(0).position + dirToPlayer;
                
                _anim.SetBool("CanMove",true);
                SetAnimationTrigger("Run");
                _ai.SetDestination(newPos);
            }
            else
            {
                //need hp regen to work

                isWander = true;
                wanderTimer = wanderDelay;
                SetAnimationTrigger("Idle");
            }
        }
        
    }

    private void FoodChoice()
    {
        isEatCrystal = FoodChoiceSet(_foodChoice.EatCrystalWhen);
        isEatDragon = FoodChoiceSet(_foodChoice.EatDragonWhen);
        isEatHuman = FoodChoiceSet(_foodChoice.EatHumanWhen);
        isEatPlant = FoodChoiceSet(_foodChoice.EatPlantWhen);
    }
    
    private bool FoodChoiceSet(float trigger)
    {
        //set food choices
        if (trigger > CurrentStomach && trigger != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    


    //only play animation (damage deal is on deal damage method)
    public override void Attack()
    {
        //if dead not attack
        base.Attack();
        
        SetAnimationTrigger("Attack");
    }

    public void RunToTarget(Transform target, string animName)
    {
        if (_anim.GetBool("CanMove") == false) return;
        if(isFlee == true) return;
        
        isWander = false;

        _ai.SetDestination(target.GetChild(0).position);
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
        if(isFlee) return;
        
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
            else
            {
                var damage = Random.Range(minDamage, maxDamage);
                
                //not special
                if (Random.Range(1, 11) > 1)
                {
                    attackTarget.Damage(damage,this.gameObject);
                    
                    DynamicTextManager.CreateText(
                        attackTarget.transform.position + Vector3.up*2,
                        damage + "",
                        TempObject.Instance.DamageTextData);
                    
                    //particle
                    var attackPar = ParticleManager.Do.SpawnParticle(
                        TempObject.Instance.BizumAttackParticle,
                        attackTarget.transform.GetChild(0).position,
                        3
                    );
                    
                    attackPar.transform.SetParent(attackTarget.transform);
                }
                //special attack
                else
                {
                    //crit
                    if (Random.Range(1, 3) == 1)
                    {
                        attackTarget.Damage(damage*2,this.gameObject);
                        DynamicTextManager.CreateText(
                            attackTarget.transform.position + Vector3.up*2,
                            "Crit! " + (maxDamage * Random.Range(1,2)),
                            TempObject.Instance.CriteTextData);
                    
                        //particle
                        var attackPar = ParticleManager.Do.SpawnParticle(
                            TempObject.Instance.BizumAttackParticle,
                            attackTarget.transform.GetChild(0).position,
                            3
                        );
                    
                        attackPar.transform.SetParent(attackTarget.transform);
                    }
                    else
                    {
                        //skill
                        StartCoroutine(UseSpecialAttack(damage));
                    }
                }
                
            }
        }
    }

    public IEnumerator UseSpecialAttack(int damage)
    {
        var attackTimes = (damage * 2) / 10;
        
        //particle
        var attackPar = ParticleManager.Do.SpawnParticle(
            TempObject.Instance.BizumCritParticle,
            attackTarget.transform.GetChild(0).position,
            3
        );
                    
        attackPar.transform.SetParent(attackTarget.transform);
        
        
        
        for (int i = 0; i < attackTimes; i++)
        {
            attackTarget.Damage(damage*2/10 + Random.Range(1,10),this.gameObject);
            DynamicTextManager.CreateText(
                attackTarget.transform.position + Vector3.up*2,
                "" + (damage*2/10) + Random.Range(1,10),
                TempObject.Instance.EffectDamageTextData);
            
            
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    private void Fly()
    {
        
    }
    
    private void Eat()
    {
        
    }

    public override void Dead()
    {
        base.Dead();
        _ai.isStopped = true;
    }

    public override void Damage(int amount, GameObject damageDealer)
    {
        attackTarget = damageDealer.GetComponent<Creature>();
        Hp -= amount;

        

        if(isFlee == false)
        {
            RunToTarget(damageDealer.transform,"Run");
        }

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
        var crystalParticle = ParticleManager.Do.SpawnParticle(
            TempObject.Instance.CrystalParticle,
            transform.GetChild(0).position,
            2);
        crystalParticle.transform.SetParent(crystal.transform);

        //set crystal size
        crystal.transform.localScale = new Vector3(Size * 10, Size * 10, Size * 10);
        
        var soulCrystal = crystal.GetComponent<SoulCrystal>();

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
        
        if(isFlee) return;

        if(isWander == false) return;
        
        //kinesis
        _ai.speed = Mathf.Lerp(MaxSpeed,0 , CurrentStomach / 10);
        
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
            SetAnimationTrigger("Walk"); }
        else if(_ai.remainingDistance <= _ai.stoppingDistance)
        {
            _anim.SetBool("CanMove",false);
            SetAnimationTrigger("Idle");

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

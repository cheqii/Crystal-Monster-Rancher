using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[ExecuteInEditMode]
public class Dragon : Creature,ICrystallizable
{
    
    [Range(0f,30f)]
    [SerializeField] private float moveSpeed; 
    [SerializeField] private GameObject renderer;
    private NavMeshAgent _ai;

    private AttackArea _attackArea;
    

    // Start is called before the first frame update
    void Awake()
    {
        _attackArea = GetComponentInChildren<AttackArea>();
        Specie = Species.Dragon;
        _ai = GetComponent<NavMeshAgent>();
        _ai.speed = moveSpeed;
        
        
        GetComponent<Animator>().SetTrigger("Idle");

    }

    
    void Update()
    {
        Material newMat = new Material(renderer.GetComponent<Renderer>().sharedMaterial);
        newMat.color = Color;
        renderer.GetComponent<Renderer>().sharedMaterial = newMat;
        
        
        _ai.velocity = _ai.desiredVelocity;
    }




    public override void Attack()
    {
        SetAnimationTrigger("Attack");
    }
    
    
    
    public override void Radar(Transform target)
    {
        _ai.isStopped = false;

        switch (target.transform.tag)
        {
            case "Player" :
                _ai.SetDestination(target.transform.position);
                attackTarget = target.GetComponent<Creature>();
                _attackArea.target = target;
                SetAnimationTrigger("Run");
                break;
            
            case "Plant" :
                _ai.SetDestination(target.transform.position);
                attackTarget = target.GetComponent<Creature>();
                _attackArea.target = target;
                SetAnimationTrigger("Walk");

                break;
        }
    }

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
            attackTarget.Damage(0);

        }
    }
    
    private void Fly()
    {
        
    }
    
    private void Eat()
    {
        
    }

    public void Crystallize()
    {
        
    }

    
 
}

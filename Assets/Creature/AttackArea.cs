using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{

    public Transform target;

    private void OnTriggerStay(Collider other)
    {
        if (target == null) return;
        
        if (other.gameObject == target.gameObject)
        {
            var creature = transform.parent.GetComponent<Creature>();
            
            creature._anim.SetBool("CanMove", false);
            
            //if dead eat if not dead attack
            if (target.GetComponent<Creature>().IsDead)
            {
                creature._anim.SetTrigger("Eat");
                creature.CurrentStomach = creature.MaxStomach;
                
                target.GetComponent<Creature>().BodyDissolve();
            }
            else
            {
                creature.Attack();

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (target == null) return;
        
        if (other.gameObject == target.gameObject)
        {
            transform.parent.GetComponent<Creature>().attackTarget = null;
            transform.parent.GetComponent<Creature>()._anim.SetBool("CanMove", true);
        }    
    }
}

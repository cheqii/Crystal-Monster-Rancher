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

            //if dead eat
            if (target.GetComponent<Creature>().IsDissolve == true) return;
            
            if (target.GetComponent<Creature>().IsDead == true)
            {
                creature._anim.SetTrigger("Eat");
                creature._anim.SetBool("IsEating",true);
                
                creature.CurrentStomach = Mathf.Clamp(
                    ((float)target.GetComponent<Creature>().MaxHp / (float)creature.MaxHp) * 10
                    ,0,
                    10);
                
                
                target.GetComponent<Creature>().BodyDissolve();
            }
            else if (target.GetComponent<Creature>().IsDead == false)
            {
                creature.Attack();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var creature = transform.parent.GetComponent<Creature>();
        
        if (target == null) return;
        
        if (other.gameObject == target.gameObject && creature.isFlee == false)
        {
            creature.attackTarget = null;
            creature._anim.SetBool("CanMove", true);
        }    
    }
}

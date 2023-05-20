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
            transform.parent.GetComponent<Creature>().Attack();
            transform.parent.GetComponent<Creature>()._anim.SetBool("CanMove", false);
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

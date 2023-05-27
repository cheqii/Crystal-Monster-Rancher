using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBullet : Bullet
{
    public override void Move(Transform bullet, Vector3 hitPoint)
    {
        GameObject cloneBullet = Instantiate(gameObject, bullet.position, bullet.rotation);
        cloneBullet.GetComponent<Rigidbody>().velocity = cloneBullet.transform.forward * Speed;

        
       // Destroy(this.gameObject,1);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<ICrystallizable>() != null)
        {
            col.GetComponent<ICrystallizable>().Crystallize();
        }
    }
}

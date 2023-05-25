using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBullet : Bullet
{
    public override void Move(Transform bullet, Vector3 hitPoint)
    {
        GameObject cloneBullet = Instantiate(gameObject, bullet.position, bullet.rotation);
        cloneBullet.GetComponent<Rigidbody>().velocity = cloneBullet.transform.forward * Speed;

        if (Vector3.Distance(hitPoint, bullet.position) >= 3)
            if(cloneBullet.gameObject.activeInHierarchy) Destroy(cloneBullet, 0.5f);
    }
    
    public void CrystalizeTarget(GameObject target)
    {
        
    }
}

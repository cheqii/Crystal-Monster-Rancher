using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBullet : Bullet
{
    public override void Move(Transform bullet)
    {
        GameObject cloneBullet = Instantiate(gameObject, bullet.position, bullet.rotation);
        cloneBullet.GetComponent<Rigidbody>().velocity = cloneBullet.transform.forward * Speed;
        
        if(cloneBullet.gameObject.activeInHierarchy) Destroy(cloneBullet, 2f);
    }
    public void CrystalizeTarget(GameObject target)
    {
        
    }
}

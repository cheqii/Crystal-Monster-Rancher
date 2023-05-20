using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBullet : Bullet
{
    public override void Move(Transform bullet)
    {
        GameObject crystalBullet = Instantiate(gameObject, bullet.position, bullet.rotation);
        crystalBullet.GetComponent<Rigidbody>().velocity = crystalBullet.transform.forward * Speed;
        
        Debug.Log("Crystal Bullet Move");
        // transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }
    public void CrystalizeTarget(GameObject target)
    {
        
    }
}

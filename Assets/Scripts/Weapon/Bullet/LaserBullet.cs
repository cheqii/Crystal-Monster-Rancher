using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class LaserBullet : Bullet
{
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;

    public override void Move(Transform bullet)
    {
        GameObject cloneBullet = Instantiate(gameObject, bullet.position, bullet.rotation);
        cloneBullet.GetComponent<Rigidbody>().velocity = cloneBullet.transform.forward * Speed;
        
        // check if the clone bullet have active in the hierarchy then destroy it after 2 seconds
        if(cloneBullet.gameObject.activeInHierarchy) Destroy(cloneBullet, 2f);
    }

    public void DealDamage(GameObject target)
    {
        int damage = Random.Range(minDamage, maxDamage);
        Debug.Log("Laser Bullet Deal Damage");
        // target.GetComponent<Enemy>().TakeDamage(damage);
    }
}

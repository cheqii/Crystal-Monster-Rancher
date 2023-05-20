using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class LaserBullet : Bullet
{
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;

    public override void Move(Transform bullet)
    {
        GameObject laser = Instantiate(gameObject, bullet.position, bullet.rotation);
        laser.GetComponent<Rigidbody>().velocity = laser.transform.forward * Speed;
        
        Debug.Log("Laser Bullet Move");
    }

    public void DealDamage(GameObject target)
    {
        int damage = Random.Range(minDamage, maxDamage);
        Debug.Log("Laser Bullet Deal Damage");
        // target.GetComponent<Enemy>().TakeDamage(damage);
    }
}

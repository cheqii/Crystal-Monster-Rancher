using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class LaserBullet : Bullet
{
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;

    public override void Move()
    {
        base.Move();
    }

    public void DealDamage(GameObject target)
    {
        int damage = Random.Range(minDamage, maxDamage);
        // target.GetComponent<Enemy>().TakeDamage(damage);
    }
}

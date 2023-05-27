using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class LaserBullet : Bullet
{
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;

    public override void Move(Transform bullet, Vector3 hitPoint)
    {
        GameObject cloneBullet = Instantiate(gameObject, bullet.position, bullet.rotation);
        cloneBullet.GetComponent<Rigidbody>().velocity = cloneBullet.transform.forward * Speed;

        if (Vector3.Distance(hitPoint, bullet.position) >= 3)
            if(cloneBullet.gameObject.activeInHierarchy) Destroy(cloneBullet, 0.5f); // check if the clone bullet have active in the hierarchy then destroy it after 2 seconds
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<IDamagable>() != null)
        {
            int damage = Random.Range(minDamage, maxDamage);
            col.gameObject.GetComponent<IDamagable>().Damage(damage, FindObjectOfType<PlayerMovement>().gameObject);

            DynamicTextManager.CreateText(
                col.transform.position + Vector3.up * 2,
                damage + "",
                TempObject.Instance.DamageTextData);
        }
    }
    

}

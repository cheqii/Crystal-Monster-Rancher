using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int speed;

    public int Speed
    {
        get => speed;
        set => speed = value;
    }
    public virtual void Move()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dragon : Creature,ICrystallizable
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private NavMeshAgent _ai;
    [SerializeField] private SexEnum.Sex _sex;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        _species = Species.Dragon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void Radar()
    {
        
    }
    
    private void Fly()
    {
        
    }
    
    private void Eat()
    {
        
    }

    public void Crystallize()
    {
        
    }
}

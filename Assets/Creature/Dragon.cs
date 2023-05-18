using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dragon : Creature,ICrystallizable
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private NavMeshAgent _ai;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        Specie = Species.Dragon;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[ExecuteInEditMode]
public class Dragon : Creature,ICrystallizable
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private NavMeshAgent _ai;


    [SerializeField] private GameObject renderer;
    
    
    
    // Start is called before the first frame update
    void Awake()
    {
        Specie = Species.Dragon;
    }

    
    void Update()
    {
        renderer.GetComponent<Renderer>().sharedMaterial.color = Color;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : Creature,ICollectable
{
    [SerializeField] private SexEnum.Sex _sex;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        Specie = Species.Plant;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Watering()
    {
        Debug.Log("not override yet (Plant.cs)");
    }


    public void Collect()
    {
        
    }
}

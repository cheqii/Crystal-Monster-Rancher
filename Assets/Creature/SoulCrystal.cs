using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulCrystal : Creature,ICollectable
{
    [SerializeField] private SexEnum.Sex _sex;
    
    // Start is called before the first frame update
    void Start()
    {
        Specie = Species.Crystal;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CrystalSetup()
    {
        
    }

    public void Collect()
    {
        
    }
}

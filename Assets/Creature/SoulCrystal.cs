using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulCrystal : Creature,ICollectable
{
    [SerializeField] private GameObject renderer;

    
    // Start is called before the first frame update
    void Start()
    {
        Specie = Species.Crystal;
    }

    // Update is called once per frame
    void Update()
    {
        Material newMat = new Material(renderer.GetComponent<Renderer>().sharedMaterial);
        newMat.color = Color;
        renderer.GetComponent<Renderer>().sharedMaterial = newMat;
    }

    public void CrystalSetup(SexEnum.Sex sex,Color color,float size,int sleepTime)
    {
        this.Sex = sex;
        this.Color = color;
        this.Size = size;
        this.SleepDelay = sleepTime;

        if (CanSleep)
        {
            StartCoroutine(SleepTimer());
        }
    }
    
    public override void Sleep()
    {
        
        
        transform.GetChild(0).gameObject.SetActive(true);
        
        //particle
        var crystalParticle =  Instantiate(TempObject.Instance.CrystalParticle, transform.position, Quaternion.identity);
        crystalParticle.transform.SetParent(transform.GetChild(0));
        TempObject.Instance.DestroyDelay(crystalParticle,2);
        
        transform.GetChild(0).SetParent(null);
        Destroy(this.gameObject);
    }

    public void Collect()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDatShit : MonoBehaviour
{
    [Header("press K To spawn")]
    public GameObject GameObject;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ParticleManager.Do.SpawnParticle(GameObject, transform.position, 10);
        }
    }
}

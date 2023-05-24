using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Do { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Do != null && Do != this)
        {
            Destroy(this);
        }
        else
        {
            Do = this;
        }
    }

    public GameObject SpawnParticle(GameObject particle,Vector3 _pos,float destroyDelays)
    {
        var par = Instantiate(particle, _pos, Quaternion.identity);
        TempObject.Instance.DestroyDelay(par,destroyDelays);
        return par;
    }
}

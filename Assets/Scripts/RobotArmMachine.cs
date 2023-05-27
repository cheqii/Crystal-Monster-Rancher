using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotArmMachine : MonoBehaviour
{
    [SerializeField] private Transform ikTarget;
    
    [Header("A->B->null->B->A->D->A (7)")]
    [SerializeField] private Transform[] movePoint;
    public GameObject targetObj;
    [SerializeField] private bool isBusy = false;
    [SerializeField] private Transform clawTip;

    
    [SerializeField] private Transform spiningRaycast;
    
    [SerializeField] private Animator _animator;
    
    [Range(0.5f,10f)]
    [SerializeField] private float speed;


    RaycastHit hit;
    

    private void Update()
    {
        Ray ray = new Ray(spiningRaycast.position, spiningRaycast.TransformDirection(Vector3.forward));
        Debug.DrawRay(spiningRaycast.position, spiningRaycast.TransformDirection(Vector3.forward)*10,Color.red);

        spiningRaycast.eulerAngles += new Vector3(0, Time.deltaTime*100, 0);

        if (Physics.Raycast(ray, out hit, 10f))
        {
            if (hit.transform.tag == "Crystal" && isBusy != true)
            {
                targetObj = hit.transform.gameObject;
                movePoint[2] = hit.transform;
                DoGrab();
            }
        }

    }

    public void DoGrab()
    {
        if(isBusy == true) return;
        StartCoroutine(GrabCrystal());
        isBusy = true;
    }

    IEnumerator GrabCrystal()
    {
        int pointNumber = 0;
        
        foreach (var i in movePoint)
        {
            pointNumber++;
            
            var t = 0.0f;
            if (pointNumber == 4)
            {
                targetObj.GetComponent<Rigidbody>().useGravity = false;
                targetObj.GetComponent<Rigidbody>().isKinematic = true;
                targetObj.transform.SetParent(clawTip);
                _animator.SetTrigger("Grab");
            }
            if (pointNumber == 7)
            {
                targetObj.GetComponent<Rigidbody>().useGravity = true;
                targetObj.GetComponent<Rigidbody>().isKinematic = false;
                targetObj.transform.SetParent(null);
            }
            
            while (t < 1.0)
            {
                t += Time.deltaTime * speed;
                ikTarget.position = Vector3.Lerp(ikTarget.position, i.position, t);

                if (pointNumber == 4)
                {
                    targetObj.transform.position = Vector3.Lerp(targetObj.transform.position, clawTip.position, t);
                }
                
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
        isBusy = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProceduralRecoil : MonoBehaviour
{
    private Vector3 currentRotation, targetRotation, targetPosition, currentPosition, initialGunPosition;
    public Transform Cam;

    [SerializeField] private float recoilX;
    [SerializeField] private float recoilY;
    [SerializeField] private float recoilZ;

    [SerializeField] private float kickBackz;

    public float snappiness, returnAmount;




    
    // Start is called before the first frame update
    void Start()
    {
        initialGunPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            Recoil();
        }
        
        
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, Time.deltaTime * returnAmount);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, Time.fixedDeltaTime * snappiness);
        transform.localRotation = Quaternion.Euler(currentRotation);
        Back();
    }


    public void Recoil()
    {
        targetPosition -= new Vector3(0, 0, kickBackz);
        targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
    }

    void Back()
    {
        targetPosition = Vector3.Lerp(targetPosition, initialGunPosition, Time.deltaTime * returnAmount);
        currentPosition = Vector3.Lerp(currentPosition, targetPosition, Time.deltaTime * snappiness);
        transform.localPosition = currentPosition;
    }
}

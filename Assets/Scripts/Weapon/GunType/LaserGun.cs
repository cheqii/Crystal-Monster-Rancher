using System.Collections;
using UnityEngine;
using TMPro;

public class LaserGun : Gun, IShoot
{
    #region -Declared Variables-

    [Header("Laser Gun Line & Laser Bullet")]
    [SerializeField] private LaserBullet laserBullet;
    [SerializeField] private Transform laserOrigin; // origin of the laser from the gun
    [SerializeField] private Transform shootPoint; // where bullet shoot from
 
    private LineRenderer _line;

    [Header("Ammo")] 
    [SerializeField] private TextMeshProUGUI ammoText;

    public TextMeshProUGUI AmmoText
    {
        get => ammoText;
        set => ammoText = value;
    }
    
    [Header("Laser Gun")]
    private float fireRate = 0.2f;
    private float nextFire;

    private GameObject mainInventory;
    private GameObject storageInventory;
    
    #endregion

    #region -Unity Event Functions-

    private void Awake()
    {
        _line = GetComponent<LineRenderer>();
    }
    
    void Start()
    {
        _Camera = GetComponentInParent<PlayerLook>().Cam;
        ammoText.text = "Ammo : " + Ammo;
    }

    void Update()
    {
        Shoot();
    }

    #endregion

    #region -Laser Shoot-

    public void Shoot()
    {
        nextFire += Time.deltaTime;
        if (GunHold != GunType.LaserGun) return;
        ammoText.enabled = true;
        if (Input.GetMouseButtonDown(0) && Ammo > 0 && nextFire > fireRate)
        {
            // if inventory or storage is open, can't shoot a gun
            var main = GameObject.FindGameObjectWithTag("MainInventory");
            var storage = GameObject.FindGameObjectWithTag("Storage");
            mainInventory = main;
            storageInventory = storage;
            
            if(main) if(mainInventory.activeSelf) return;
            if(storage) if(storageInventory.activeSelf) return;
            
            Ammo -= 1;
            ammoText.text = "Ammo : " + Ammo;
            _line.SetPosition(0, laserOrigin.position); // set line origin to laser origin
            Vector3 rayOrigin = _Camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f)); // center of the camera to create ray origin
            RaycastHit hit;

            if (Physics.Raycast(rayOrigin, _Camera.transform.forward, out hit, Distance))
            {
                
                _line.SetPosition(1, hit.point); // set end position of line to hit point
                laserBullet.DealDamage(hit.transform.gameObject);
                Destroy(hit.transform.gameObject);
                if (laserBullet.gameObject.activeInHierarchy) Destroy(laserBullet);
            }
            else // set end position of line to ray origin -> distance
            {
                _line.SetPosition(1, rayOrigin + (_Camera.transform.forward * Distance));
            }
            
            laserBullet.Move(shootPoint, _line.GetPosition(1)); // Instantiate laser bullet and shoot it to the target
            StartCoroutine(LaserDelay()); // delay for laser line
        }
        
        if(Ammo <= 0)
            Debug.Log("Out of Ammo!");
    }

    IEnumerator LaserDelay()
    {
        _line.enabled = true;
        yield return new WaitForSeconds(AmmoDelay);
        _line.enabled = false;
    }

    #endregion
    
}

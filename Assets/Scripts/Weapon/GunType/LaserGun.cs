using System.Collections;
using UnityEngine;
using TMPro;

public class LaserGun : Gun, IShoot
{
    [Header("Laser Gun Line & Laser Bullet")]
    [SerializeField] private LaserBullet laserBullet;
    [SerializeField] private Transform laserOrigin;
    
    private LineRenderer _line;

    [Header("Ammo")] 
    [SerializeField] private TextMeshProUGUI ammoText;
    
    [Header("Laser Gun")]
    private float fireRate = 0.2f;
    private float nextFire;
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

    #region -Laser Shoot-

    public void Shoot()
    {
        nextFire += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && Ammo > 0 && nextFire > fireRate)
        {
            Ammo -= 1;
            ammoText.text = "Ammo : " + Ammo;
            _line.SetPosition(0, laserOrigin.position);
            Vector3 rayOrigin = _Camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit hit;
        
            if (Physics.Raycast(rayOrigin, _Camera.transform.forward, out hit, Distance))
            {
                _line.SetPosition(1, hit.point);
                Destroy(hit.transform.gameObject);
                Debug.Log("Destroy and Laser line");
            }
            else
            {
                _line.SetPosition(1, rayOrigin + (_Camera.transform.forward * Distance));
            }

            StartCoroutine(ShootDelay());
            Debug.Log("Create Laser Line!");
        }
        
        if(Ammo <= 0)
            Debug.Log("Out of Ammo!");
    }

    IEnumerator ShootDelay()
    {
        _line.enabled = true;
        yield return new WaitForSeconds(AmmoDelay);
        _line.enabled = false;
    }

    #endregion
}

using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
public class Weapon : MonoBehaviour 
{
    public enum GunType { Semi, Auto, Shotgun };
    public LayerMask collisionMask;
    public float gunID;
    public GunType gunType;
    public float rpm;
    public float damage = 1;
    public int totalAmmo = 40;
    public int ammoPerMag = 10;
    public Transform spawn;
    public Transform shellEjectionPoint;
    public Rigidbody shell;
    private LineRenderer tracer;
    public int bulletForce = 500;
    private float secondsBetweenShots;
    private float nextPossibleShootTime;
    private int currentAmmoInMag;
    private bool reloading;

    void Start()
    {
        secondsBetweenShots = 60 / rpm;
        if (GetComponent<LineRenderer>())
        {
            tracer = GetComponent<LineRenderer>();
        }

        currentAmmoInMag = ammoPerMag;

    }

    public void Shoot()
    {

        if (CanShoot())
        {
            Debug.Log("hit");
            Ray ray = new Ray(spawn.position, spawn.forward);
            RaycastHit hit;

            float shotDistance = 20;

            if (Physics.Raycast(ray, out hit, shotDistance, collisionMask))
            {
                shotDistance = hit.distance;

                //if (hit.collider.GetComponent<EnemyHealth>())
                //{
                //    hit.collider.GetComponent<EnemyHealth>().TakeDamage(damage);
                //}
            }

            nextPossibleShootTime = Time.time + secondsBetweenShots;
            currentAmmoInMag--;

            GetComponent<AudioSource>().Play();

            if (tracer)
            {
                StartCoroutine("RenderTracer", ray.direction * shotDistance);
            }

            Rigidbody newShell = Instantiate(shell, shellEjectionPoint.position, Quaternion.identity) as Rigidbody;
            newShell.AddForce(bulletForce*(shellEjectionPoint.forward + spawn.forward ));
            Debug.Log(Random.Range(150f, 200f));


        }

    }

    public void ShootContinuous()
    {
        if (gunType == GunType.Auto)
        {
            Shoot();
        }
    }

    public void Spread()
    {
        if (gunType == GunType.Shotgun)
        {
            Shoot();        
        }
    }

    //public void Spread()
    //{
    //    if (gunType == GunType.Shotgun)
        
    //         Vector3 Spread ( Vector3 aim ,   float distance ,   float variance  )
    // aim.Normalize();
    // Vector3 v3;
    // do 
    //     v3 = Random.insideUnitSphere;
    // while (v3 == aim || v3 == -aim);
    // v3 = Vector3.Cross(aim, v3);
    // v3 = v3 * Random.Range(0.0f, variance);
    // return aim * distance + v3;
 
        
    //}
    
         

    private bool CanShoot()
    {
        bool canShoot = true;

        Debug.Log("can't shoot");
        if (Time.time < nextPossibleShootTime)
        {
            Debug.Log("waiting");
            canShoot = false;
        }

        if (currentAmmoInMag == 0)
        {
            Debug.Log("No ammo");
            canShoot = false;
        }

        if (reloading)
        {
            Debug.Log("reloading");
            canShoot = false;
        }


        return canShoot;
    }

    void Update()
    {
        if (Input.GetButtonDown("Shoot"))
        {
            Shoot();
        }
        else if (Input.GetButton("Shoot"))
        {
            ShootContinuous();
        }
    }

    public bool Reload()
    {
        if (totalAmmo != 0 && currentAmmoInMag != ammoPerMag)
        {
            reloading = true;
            return true;
        }

        return false;
    }

    public void FinishReload()
    {
        reloading = false;
        currentAmmoInMag = ammoPerMag;
        totalAmmo -= ammoPerMag;

        if (totalAmmo < 0)
        {
            currentAmmoInMag += totalAmmo;
            totalAmmo = 0;
        }

    }

    IEnumerator RenderTracer(Vector3 hitPoint)
    {
        //tracer.enabled = true;
        tracer.SetPosition(0, spawn.position);
        tracer.SetPosition(1, spawn.position + hitPoint);

        yield return null;
        tracer.enabled = false;
    }

    /*
	public enum GunType {Auto,Semi,Burst, Shutgun};
	public GunType gunType;
	public float rpm;
	public Transform spawn;
	public Transform bulletEjectionPoint;
	public Rigidbody bullet;
    public Weapon weapon;
	private LineRenderer tracer;
	private float secondsBetweenShots;
	private float nextPossibleShootTime;
	
	void Start() {
		secondsBetweenShots = 60/rpm;
		if (GetComponent<LineRenderer>()) {
			tracer = GetComponent<LineRenderer>();
		}
	}

    void Update()
    {
        if (Input.GetButtonDown("Shoot"))
        {
            Shoot();
        }
        else if (Input.GetButton("Shoot"))
        {
            ShootContinuous();
        }
    }
	
	public void Shoot() {
		
		if (CanShoot()) {
			Ray ray = new Ray(spawn.position,spawn.forward);
			RaycastHit hit;
			
			float shotDistance = 20;
			
			if (Physics.Raycast(ray,out hit, shotDistance)) {
				shotDistance = hit.distance;
			}
			
			nextPossibleShootTime = Time.time + secondsBetweenShots;
			
			GetComponent<AudioSource>().Play();
			
			if (tracer) {
				StartCoroutine("RenderTracer", ray.direction * shotDistance);
			}
			
			Rigidbody newBullet = Instantiate(bullet,bulletEjectionPoint.position,Quaternion.identity) as Rigidbody;
			newBullet.AddForce(bulletEjectionPoint.forward * Random.Range(150f,200f) + spawn.forward * Random.Range(-10f,10f));
		}
		
	}
	
	public void ShootContinuous() {
		if (gunType == GunType.Auto) {
			Shoot ();
		}
	}
	
	private bool CanShoot() {
		bool canShoot = true;
		
		if (Time.time < nextPossibleShootTime) {
			canShoot = false;
		}
		
		return canShoot;
	}
	
	IEnumerator RenderTracer(Vector3 hitPoint) {
		tracer.enabled = true;
		tracer.SetPosition(0,spawn.position);
		tracer.SetPosition(1,spawn.position + hitPoint);
		
		yield return null;
		tracer.enabled = false;
	}*/
}


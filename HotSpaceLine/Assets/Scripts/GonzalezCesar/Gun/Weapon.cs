using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
public class Weapon : MonoBehaviour 
{
	public enum GunType {Semi,Burst,Auto};
	//public float gunID;
	public GunType gunType;
	public float rpm;
	public int totalAmmo = 40;
	public int ammoPerMag = 10;
    public float bulletVelocity = 10;
    public int dmgPerShot = 10;
	public Transform spawn;
	public Transform shellEjectionPoint;
	public GameObject shell;
	private LineRenderer tracer;
	private float secondsBetweenShots;
	private float nextPossibleShootTime;
	public int currentAmmoInMag;
    public int reloadTime;
    public bool reloading = false;
    public int accVariance;
    public int shotsPerFire;
    public bool shotFired = false;
	
	void Start() {
        secondsBetweenShots = 60/rpm;
        //if (GetComponent<LineRenderer>()) {
        //    tracer = GetComponent<LineRenderer>();
        //}
		
        currentAmmoInMag = ammoPerMag;

	}
	
	public void Shoot() {
		
		if (CanShoot()) {
            nextPossibleShootTime = Time.time + secondsBetweenShots;
            currentAmmoInMag--;
            GetComponent<AudioSource>().Play();
            shotFired = true;
            StartCoroutine(WaitAndFalse(2));
            for (int i = 0; i < shotsPerFire; i++)
            {
                Ray ray = new Ray(spawn.position, spawn.forward);
                RaycastHit hit;

                float shotDistance = 10000000;

                /*if (Physics.Raycast(ray, out hit, shotDistance))
                {
                    shotDistance = hit.distance;

                    if (hit.collider.tag == "Player")
                    {
                        hit.collider.GetComponent<PlayerHealth>().subCurrentHealth(dmgPerShot);
                    }
                    if (hit.collider.tag == "Enemy")
                    {
                        hit.collider.GetComponent<EnemyHealth>().subCurrentHealth(dmgPerShot);
                    }
                }*/



                if (Physics.Raycast(ray, out hit, shotDistance))
                {
                    shotDistance = hit.distance;
                } 
                if (tracer)
                {
                    StartCoroutine("RenderTracer", ray.direction * shotDistance);
                }

                GameObject newShell = (GameObject) Instantiate(shell, shellEjectionPoint.position, Quaternion.identity);
                Vector3 accVector = new Vector3(Random.Range(-accVariance, accVariance), 0, Random.Range(-accVariance, accVariance));
                newShell.GetComponent<Rigidbody>().AddForce((shellEjectionPoint.forward * bulletVelocity) + accVector);
            }
		}
		
	}
	
	public void ShootContinuous() {
		if (gunType == GunType.Auto) {
			Shoot ();
		}
	}
	
	public bool CanShoot() {
		bool canShoot = true;

		if (Time.time < nextPossibleShootTime) {
			canShoot = false;
		}
		
		if (currentAmmoInMag == 0) {
			canShoot = false;
		}
		
		if (reloading) {
			canShoot = false;
		}
		
		
		return canShoot;
	}
	
	public bool Reload() {
		if (totalAmmo != 0 && currentAmmoInMag != ammoPerMag && reloading == false) {
            Debug.Log("WE CAN RELOAD");
                return true;
		}
		return false;
	}
	
	public void FinishReload() {
        reloading = true;
        StartCoroutine(ReloadTime(reloadTime));
	}
	
	IEnumerator RenderTracer(Vector3 hitPoint) {
		tracer.enabled = true;
		tracer.SetPosition(0,spawn.position);
		tracer.SetPosition(1,spawn.position + hitPoint);
		
		yield return null;
		tracer.enabled = false;
	}
    IEnumerator WaitAndFalse(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        shotFired = false;
    }
    IEnumerator ReloadTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        reloading = false;
        currentAmmoInMag = ammoPerMag;
        totalAmmo -= ammoPerMag;

        if (totalAmmo < 0)
        {
            currentAmmoInMag += totalAmmo;
            totalAmmo = 0;
        }
    }
}


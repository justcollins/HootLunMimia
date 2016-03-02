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
	public Transform spawn;
	public Transform shellEjectionPoint;
	public Rigidbody shell;
	private LineRenderer tracer;
	private float secondsBetweenShots;
	private float nextPossibleShootTime;
	private int currentAmmoInMag;
	private bool reloading;
	
	void Start() {
		secondsBetweenShots = 60/rpm;
		if (GetComponent<LineRenderer>()) {
			tracer = GetComponent<LineRenderer>();
		}
		
		currentAmmoInMag = ammoPerMag;

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
			currentAmmoInMag --;
			
			GetComponent<AudioSource>().Play();
			
			if (tracer) {
				StartCoroutine("RenderTracer", ray.direction * shotDistance);
			}
			
			Rigidbody newShell = Instantiate(shell,shellEjectionPoint.position,Quaternion.identity) as Rigidbody;
			newShell.AddForce(shellEjectionPoint.forward * Random.Range(150f,200f) + spawn.forward * Random.Range(-10f,10f));
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
		
		if (currentAmmoInMag == 0) {
			canShoot = false;
		}
		
		if (reloading) {
			canShoot = false;
		}
		
		
		return canShoot;
	}
	
	public bool Reload() {
		if (totalAmmo != 0 && currentAmmoInMag != ammoPerMag) {
			reloading = true;
			return true;
		}
		
		return false;
	}
	
	public void FinishReload() {
		reloading = false;
		currentAmmoInMag = ammoPerMag;
		totalAmmo -= ammoPerMag;
		
		if (totalAmmo < 0) {
			currentAmmoInMag += totalAmmo;
			totalAmmo = 0;
		}
	}
	
	IEnumerator RenderTracer(Vector3 hitPoint) {
		tracer.enabled = true;
		tracer.SetPosition(0,spawn.position);
		tracer.SetPosition(1,spawn.position + hitPoint);
		
		yield return null;
		tracer.enabled = false;
	}
}


using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
public class Weapon : MonoBehaviour {

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
        //ControlMouse();

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
	}
}


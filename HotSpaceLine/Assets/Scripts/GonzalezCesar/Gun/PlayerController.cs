using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
public class PlayerController : MonoBehaviour 
{

	public float rotationSpeed = 450;
	public float walkSpeed = 5;
	public float runSpeed = 8;
	private float acceleration = 5;
	private Quaternion targetRotation;
	private Vector3 currentVelocityMod;
	private bool reloading;
	public Transform hand;
	public Weapon[] guns;
	private Weapon currentGun;
	private CharacterController controller;

	void Start () 
	{
		controller = GetComponent<CharacterController>();
		EquipGun(0);
	}

	void Update () 
	{
		ControlMouse();
		if (currentGun) {
			if (Input.GetButtonDown("Shoot")) 
			{
				currentGun.Shoot();
			}
			else if (Input.GetButton("Shoot")) 
			{
				currentGun.ShootContinuous();
			}

			if (Input.GetButtonDown("Reload")) 
			{
				if (currentGun.Reload()) 
				{
					reloading = true;
				}
			}

			if (reloading) 
			{ 
					currentGun.FinishReload();
					reloading = false;
			}
		}

		for (int i = 0; i < guns.Length; i++) 
		{
			if (Input.GetKeyDown((i+1) + "") || Input.GetKeyDown("[" + (i+1) + "]")) 
			{
				EquipGun(i);
				break;
			}
		}	
	}

	void EquipGun(int i) 
	{
		if (currentGun) 
		{
			Destroy(currentGun.gameObject);
		}

		currentGun = Instantiate(guns[i],hand.position,hand.rotation) as Weapon;
		currentGun.transform.parent = hand;
	}

	void ControlMouse() 
	{

		Vector3 mousePos = Input.mousePosition;
		targetRotation = Quaternion.LookRotation(mousePos - new Vector3(transform.position.x,0,transform.position.z));
		transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y,targetRotation.eulerAngles.y,rotationSpeed * Time.deltaTime);

		Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));

		currentVelocityMod = Vector3.MoveTowards(currentVelocityMod,input,acceleration * Time.deltaTime);
		Vector3 motion = currentVelocityMod;
		motion *= (Mathf.Abs(input.x) == 1 && Mathf.Abs(input.z) == 1)?.7f:1;
		motion *= (Input.GetButton("Run"))?runSpeed:walkSpeed;
		motion += Vector3.up * -8;
		
		controller.Move(motion * Time.deltaTime);
	}

}

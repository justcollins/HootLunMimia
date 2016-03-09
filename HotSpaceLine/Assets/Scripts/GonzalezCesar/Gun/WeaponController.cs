using UnityEngine;
using System.Collections;


public class WeaponController : MonoBehaviour 
{
	private Quaternion targetRotation;
	private Vector3 currentVelocityMod;
	private bool reloading;
	public Transform hand;
	//public int damage;
	public Weapon[] guns;
	private Weapon currentGun;
	private CharacterController controller;
    public bool[] weaponHave;

	void Start () 
	{
		controller = GetComponent<CharacterController>();
		EquipGun(0);
	}

	void Update () 
	{
		if (currentGun) {
			if (Input.GetButtonDown("Shoot")) 
			{
                //Debug.Log("mouse click");
				currentGun.Shoot();
			}
			else if (Input.GetButton("Shoot")) 
			{
				currentGun.ShootContinuous();
			}

			if (Input.GetButtonDown("Reload") || currentGun.currentAmmoInMag == 0) 
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
                if (weaponHave[i])
                {
                    EquipGun(i);
                    break;
                }
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
    public void setWeps(bool newWep, string wepName)
    {
        for (int i = 0; i < guns.Length; i++)
        {
            if (wepName == guns[i].name)
                weaponHave[i] = newWep;
        }
    }
}

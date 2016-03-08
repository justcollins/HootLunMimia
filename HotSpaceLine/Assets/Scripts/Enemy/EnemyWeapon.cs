using UnityEngine;
using System.Collections;


public class EnemyWeapon : MonoBehaviour
{
    private bool reloading;
    public Transform hand;
    public Weapon[] guns;
    private Weapon currentGun;
    private EnemyPathing enemy;
    private EnemySight enemySightSeal;
    public bool[] weaponHave;

    void Start()
    {
        enemy = GetComponent<EnemyPathing>();
        enemySightSeal = GetComponent<EnemySight>();
        EquipGun(0);
    }

    void Update()
    {
        if (currentGun)
        {
            if (enemySightSeal.playerInSight && currentGun.gunType != Weapon.GunType.Auto)
            {
                currentGun.Shoot();
            }
            else if (enemySightSeal.playerInSight && currentGun.gunType == Weapon.GunType.Auto)
            {
                currentGun.ShootContinuous();
            }

            if (currentGun.currentAmmoInMag == 0)
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
    }

    void EquipGun(int i)
    {
        if (currentGun)
        {
            Destroy(currentGun.gameObject);
        }

        currentGun = Instantiate(guns[i], hand.position, hand.rotation) as Weapon;
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

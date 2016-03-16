using UnityEngine;
using System.Collections;

public class EnemyHealth : Health {

    public GameObject weaponDrop;

	void Start () {
        currentHealth = health;
    }
	
	void Update () {
        AliveCheck();
        IsDead();
	}

    private void AliveCheck() {
        if (currentHealth <= 0) {
            isAlive = false;
        }
    }

    private void IsDead() {
        if (!isAlive) {
            GameObject droppedWeapon = (GameObject)Instantiate(weaponDrop, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}

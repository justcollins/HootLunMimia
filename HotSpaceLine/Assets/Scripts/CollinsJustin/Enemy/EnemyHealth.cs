using UnityEngine;
using System.Collections;

public class EnemyHealth : Health {

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
            Destroy(this.gameObject);
        }
    }
}

using UnityEngine;
using System.Collections;

public class PlayerHealth : Health {

	void Start () {
        currentHealth = health;
	}
	
	void Update () {
        AliveCheck();
        IsDead();
	}

    private void AliveCheck() {
        if(currentHealth <= 0) {
            isAlive = false;
        }
    }

    private void IsDead() {
        if (!isAlive) {
            Application.LoadLevel(0);
        }
    }
}

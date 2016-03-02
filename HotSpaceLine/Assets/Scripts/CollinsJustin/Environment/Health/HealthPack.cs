using UnityEngine;
using System.Collections;

public class HealthPack : MonoBehaviour {

    public int healAmount = 25;

    private PlayerHealth playerHealth;

	void Start () {
        playerHealth = GameObject.FindObjectOfType<PlayerHealth>();
	}

    private void OnTriggerEnter(Collider player) {
        if (player.gameObject.tag == "Player") {
            AddHealth();
        }
    }

    private void AddHealth() {
        playerHealth.addCurrentHealth(healAmount);
    }
}

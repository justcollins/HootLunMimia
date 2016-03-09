using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    public int health = 100;

    protected int currentHealth;
    protected bool isAlive = true;

    public void addCurrentHealth(int addHealth) {
        currentHealth += addHealth;
    }

    public void subCurrentHealth(int subHealth) {
        currentHealth -= subHealth;
    }

    private void AliveStatus() {
        if (currentHealth <= 0) {
            isAlive = false;
        }
    }
}

using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    public int health = 100;

    protected int currentHealth;
    protected bool isAlive = true;

    public void addCurrentHealth(int addHealth) {
        currentHealth += addHealth;
    }

    public virtual void subCurrentHealth(int subHealth) {
        currentHealth -= subHealth;
    }

    private void AliveStatus() {
        if (currentHealth <= 0) {
            isAlive = false;
        }
    }

//	public virtual void TakeDamage(float dmg) {
//		health -= dmg;
//		
//		if (health <= 0) {
//			Die();
//		}
//	}
//	
//	public virtual void Die() {
//		Destroy(gameObject);
//	}
}

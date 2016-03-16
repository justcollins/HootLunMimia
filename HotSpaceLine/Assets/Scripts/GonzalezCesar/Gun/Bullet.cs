using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public int damage;
	private float lifeTime = 5;
	private Material mat;
	private Color originalCol;
	private float fadePercent;
	private float deathTime;
	private bool fading;
    private int mod = 1;
	
	void Start () {
		mat = GetComponent<Renderer>().material;
		originalCol = mat.color;
		deathTime = Time.time + lifeTime;
		StartCoroutine("Fade");
	}
	
	IEnumerator Fade() {
		while (true) {
			yield return new WaitForSeconds(.2f);
			if (fading ){
				fadePercent += Time.deltaTime;
				mat.color = Color.Lerp(originalCol,Color.clear,fadePercent);
				
				if (fadePercent >= 1) {
					Destroy(gameObject);
				}
			}
			else {
				if (Time.time > deathTime) {
					fading = true;
				}
			}
		}
	}
	
	void OnTriggerStay(Collider c) {
        if(PlayerDamage.doubleDamage == true) {
            mod = 2;
        } else {
            mod = 1;
        }
        if (!c.isTrigger)
        {
            if(c.tag == "Enemy")
            {
                c.GetComponent<EnemyHealth>().subCurrentHealth(damage * mod);
            } else if (c.tag == "Player")
            {
                c.GetComponent<PlayerHealth>().subCurrentHealth(damage);
            }
            Destroy(gameObject);
        }
		if (c.tag == "Ground") {
			GetComponent<Rigidbody>().Sleep();
		}
	}
}

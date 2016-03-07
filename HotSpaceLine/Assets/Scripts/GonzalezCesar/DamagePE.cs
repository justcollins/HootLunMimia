using UnityEngine;
using System.Collections;

public class DamagePE : MonoBehaviour 
{

	public int playerHealth = 100;
	public int bossHealth = 115;
	public int enemyHealth = 80;
	int dmg = 10;

	void Start () 
	{
		Debug.Log (playerHealth);
		Debug.Log (enemyHealth);
	
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.tag == "Enemy") 
		{
			playerHealth -= dmg;
			Debug.Log ("Huy enemy touch me" + playerHealth);
		}

		if (playerHealth <= 0) 
		{
			Debug.Log ("Player im dead");
			Destroy(gameObject);
		}
	
	
		if (col.gameObject.tag == "Bullet") 
		{
			enemyHealth -= dmg; 
			Debug.Log ("Enemy im shot" + enemyHealth);
		}

		if (enemyHealth <= 0) 
		{
			Debug.Log ("Enemy im dead");
			Destroy(gameObject);
		}
	}

	void Update () 
	{
	
	}
}

using UnityEngine;
using System.Collections;

public class doubleDmg : MonoBehaviour {

	public void OnTriggerEnter(Collider Col) 
	{
        Debug.Log("BUTTS!");
		if(Col.tag == "Player") 
		{
            Debug.Log("butts");
            Destroy(this.gameObject);
            PlayerDamage.doubleDamage = true;
            
		}
	}
}

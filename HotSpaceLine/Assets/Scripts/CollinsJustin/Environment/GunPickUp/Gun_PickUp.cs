using UnityEngine;
using System.Collections;

public class Gun_PickUp : MonoBehaviour {

    public WeaponController player;

    private bool pickedUp = true;
	
    private void OnTriggerEnter(Collider playerCol) {
        if(playerCol.tag == "Player") {
            player.setWeps(pickedUp, this.gameObject.name);
        }
        Destroy(this.gameObject);
    }
}

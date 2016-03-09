using UnityEngine;
using System.Collections;

public class Gun_PickUp : MonoBehaviour {

    private WeaponController player;

    private void Start() {
        player = GameObject.FindObjectOfType<WeaponController>();
    }

    private bool pickedUp = true;
	
    private void OnTriggerEnter(Collider playerCol) {
        if(playerCol.tag == "Player") {
            player.setWeps(pickedUp, this.gameObject.name);
            Destroy(this.gameObject);
        }
    }
}

using UnityEngine;
using System.Collections;

public class Gun_PickUp : MonoBehaviour {

    public bool smg;
    public bool shotgun;

    private WeaponController player;
    private bool pickedUp = true;

    private void Start() {
        player = GameObject.FindObjectOfType<WeaponController>();
    }


	
    private void OnTriggerEnter(Collider playerCol) {
        if(playerCol.tag == "Player") {
            if(smg) {
                player.setWeps(pickedUp, "SMG");
            }
            if(shotgun) {
                player.setWeps(pickedUp, "Shotgun");
            }
            
            Destroy(this.gameObject);
        }
    }
}

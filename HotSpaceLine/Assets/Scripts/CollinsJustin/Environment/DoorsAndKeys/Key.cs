using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour{

    private KeyManager keyManager;

    private void Start() {
        keyManager = GameObject.FindObjectOfType<KeyManager>();
    }

    private void OnTriggerEnter(Collider player) {
        if (player.gameObject.tag == "Player") {
            keyManager.KeyPickedUp(gameObject.name);
        }
        Destroy(this.gameObject);
    }
}

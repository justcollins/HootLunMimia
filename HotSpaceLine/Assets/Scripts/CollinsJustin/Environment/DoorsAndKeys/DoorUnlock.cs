using UnityEngine;
using System.Collections;

public class DoorUnlock : MonoBehaviour {

    public GameObject matchingKey;
    private KeyManager keyManager;

    private void Start() {
        keyManager = GameObject.FindObjectOfType<KeyManager>();
    }

    private void OnTriggerEnter() {

    }

}

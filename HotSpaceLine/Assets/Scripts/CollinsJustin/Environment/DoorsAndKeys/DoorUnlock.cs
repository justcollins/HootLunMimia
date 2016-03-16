using UnityEngine;
using System.Collections;

public class DoorUnlock : MonoBehaviour {

    public float doorOpenAngle;
    public float openSpeed;

    private KeyManager keyManager;
    private Quaternion doorOpen = Quaternion.identity;
    private string keyName;
    private bool doorOpening = false;

    public void setKey(string doorKey) {
        keyName = doorKey;
    }

    private void Awake() {
        keyManager = GameObject.FindObjectOfType<KeyManager>();
        doorOpen = Quaternion.Euler(0, doorOpenAngle, 0);
    }

    private void Update() {
        OpenDoor();
    }

    private void OnTriggerEnter(Collider player) {
        if(player.tag == "Player") {
            if(keyManager.CanOpenDoor(keyName)) {
                doorOpening = true;
            }
        }
    }

    private void OpenDoor() {
        if (doorOpening)
        {
            this.gameObject.transform.localRotation = Quaternion.Slerp(this.transform.localRotation, doorOpen, Time.deltaTime * openSpeed);
            if (this.gameObject.transform.localRotation == doorOpen)
            {
                doorOpening = false;
            }
        }
    }

}

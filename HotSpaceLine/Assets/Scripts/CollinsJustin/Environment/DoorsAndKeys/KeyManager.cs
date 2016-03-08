using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyManager : MonoBehaviour {

    public GameObject[] keyObjects;
    public DoorUnlock[] lockedDoors;

    private List<string> keyNames;
    private List<bool> hasKey;

    private void Start () {
        keyNames = new List<string>();
        hasKey = new List<bool>();
	    for(int i = 0; i < keyObjects.Length; i++) {
            bool tempBool = new bool();
            hasKey.Add(tempBool);
            hasKey[i] = false;
            keyNames.Add(keyObjects[i].name);
            lockedDoors[i].setKey(keyNames[i]);
        }
    }

    public void KeyPickedUp(string name) {
        for (int i = 0; i < keyNames.Count; i++) {
            if(name == keyNames[i]) {
                hasKey[i] = true;
            }
        }
    }

    public bool CanOpenDoor(string name) {
        for (int i = 0; i < keyNames.Count; i++) {
            if (name == keyNames[i]) {
                return hasKey[i];
            }
        }
        return false;
    }


}

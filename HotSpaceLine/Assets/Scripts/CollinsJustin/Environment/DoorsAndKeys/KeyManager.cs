using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyManager : MonoBehaviour {

    public string[] keyNames;

    private List<bool> hasKey;

	void Start () {
        hasKey = new List<bool>();
	    for(int i = 0; i < keyNames.Length; i++) {
            bool tempBool = new bool();
            hasKey.Add(tempBool);
            hasKey[i] = false;
            Debug.Log("key #" + i + " is " + hasKey[i]);
        }
        /*Debug.Log("time to set a value to true");
        hasKey[0] = true;
        hasKey[2] = true;
        for (int i = 0; i < keyNames.Length; i++) {
            Debug.Log("key #" + i + " is " + hasKey[i]);
        } */
    }

    public void KeyPickedUp(string name) {
        for (int i = 0; i < keyNames.Length; i++) {
            if(name == keyNames[i]) {
                hasKey[i] = true;
            }
        }
    }

    public bool CanOpenDoor(string name) {
        for (int i = 0; i < keyNames.Length; i++) {
            if (name == keyNames[i]) {
                return hasKey[i];
            }
        }
        return false;
    }


}

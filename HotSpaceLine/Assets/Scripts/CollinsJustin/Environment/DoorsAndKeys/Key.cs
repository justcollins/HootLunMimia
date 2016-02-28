using UnityEngine;
using System.Collections;

[System.Serializable]
public class Key {

    public string name;

    private bool hasKey;

    public void setKey(bool keySet) {
        hasKey = keySet;
    }
}

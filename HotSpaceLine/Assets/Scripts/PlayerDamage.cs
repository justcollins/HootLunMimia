using UnityEngine;
using System.Collections;

public class PlayerDamage : MonoBehaviour {

    public static bool doubleDamage;
    public float doubleDamageTimer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(doubleDamage);
        if(doubleDamage) {
            StartCoroutine(WaitAndFalse(doubleDamageTimer));
        }
	}

    IEnumerator WaitAndFalse(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        doubleDamage = false;
    }
}

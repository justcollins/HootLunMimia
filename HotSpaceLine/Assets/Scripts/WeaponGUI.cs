using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponGUI : MonoBehaviour 
{
    public Text ammoText;

    public void SetAmmoInfo(int totalAmmo, int ammoInMag)
    {
        ammoText.text = ammoInMag.ToString() + "/" + totalAmmo.ToString();
    }

}

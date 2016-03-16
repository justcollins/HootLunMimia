using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//using System.Collections.Generic;

public class HealthGUI : MonoBehaviour {

	public Slider healthBar;
	private PlayerHealth playerHP;


    void Start()
    {

		healthBar.value = 100;
        playerHP = GameObject.FindObjectOfType<PlayerHealth>();

    }

	void setHealthBar()
	{
		healthBar.value = playerHP.getCurHelth();
        Debug.Log("EyyyBBBBBB"+playerHP.getCurHelth());
	}

    void Update()
    {
        //playerHP.subCurrentHealth (1);
        Debug.Log("NiceBUtt");
        setHealthBar();
		//addjustHealth();
    }

//	public void addjustHealth(int adjt)
//	{
//		healthBar += adjt;
//
//		if (healthBar < 0)
//			healthBar = 0;
//
//		if (playerHP.subCurrentHealth > playerHP.getCurHealth)
//			playerHP.subCurrentHealth = playerHP.getCurHealth;
//
//		if (playerHP.subCurrentHealth < 1)
//			playerHP.subCurrentHealth = 1;
//
//	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUIBehaviour : MonoBehaviour {
    Text HealthText;
    Text AmmoText;
    public PlayerStatusManager playerstatusManager;
    public PlayerShootHandler playerShootHandler;
    private void Awake()
    {
        HealthText = transform.Find("HealthText").GetComponent<Text>();
        AmmoText = transform.Find("AmmoText").GetComponent<Text>();
    }

    private void Update()
    {
        HealthText.text = "Health : " + playerstatusManager.currentHealth + " / " + playerstatusManager.maxHealth;
        AmmoText.text = "Ammo : " + playerShootHandler.currentAmmo + " / " + playerShootHandler.maxAmmo;
    }
}

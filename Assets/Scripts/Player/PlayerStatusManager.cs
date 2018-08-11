using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusManager : MonoBehaviour {

    public float maxHealth = 100f;
    public float currentHealth;
    public float attackPower;
    public float Energy = 100;

    private void Awake()
    {
        PlayerEventManager.OnPlayerHit += PlayerReceiveDmg;
    }

    private void PlayerReceiveDmg(float dmg)
    {
        
    }



}

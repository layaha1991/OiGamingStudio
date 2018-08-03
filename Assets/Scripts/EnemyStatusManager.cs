using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusManager : MonoBehaviour {

    public float maxHealth = 100;
    public float currentHealth = 100;
    public float attackPower = 10;

    private void Awake()
    {
        EnemyEventManager.OnDamage += HandleDamage;
    }

    private void HandleDamage(float dmg) {
        currentHealth -= dmg;
    }

}

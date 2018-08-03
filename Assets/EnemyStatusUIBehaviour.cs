using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyStatusUIBehaviour : MonoBehaviour {
    public EnemyStatusManager enemyStatusManager;
    Text HealthText;
	private void Awake () {
        HealthText = transform.Find("HealthText").GetComponent<Text>();
	}
	
	private void Update () {
        HealthText.text = "Health : " + enemyStatusManager.currentHealth + " / " + enemyStatusManager.maxHealth;

    }
}

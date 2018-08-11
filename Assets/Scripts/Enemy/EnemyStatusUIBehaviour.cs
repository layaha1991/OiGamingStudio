using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyStatusUIBehaviour : MonoBehaviour {
    EnemyStatusManager enemyStatusManager;
    Text HealthText;
	private void Awake () 
    {
        HealthText = transform.Find("HealthText").GetComponent<Text>();
	}

    private void Start()
    {
        
    }

    private void Update () 
    {
        GetEnemyStatusManager();
        if (GameObject.FindGameObjectWithTag("Enemy") != null)
        {
            HealthText.text = "Health : " + enemyStatusManager.currentHealth + " / " + enemyStatusManager.maxHealth;
        }
     
    }

    private void GetEnemyStatusManager()
    {
        if(GameObject.FindGameObjectWithTag("Enemy")!= null)
        {
            enemyStatusManager = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyStatusManager>();
        }
        else
        {
            return;
        }
      

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageReceiver : MonoBehaviour 
{
    public float damageMultiplier;
    EnemyEventManager enemyEventManager;

    [SerializeField]
    private Transform _blood;

    private void Awake()
    {
        enemyEventManager = FindEventManagerRecursively(transform);
        _blood = this.gameObject.transform.GetChild(0);
    }

    EnemyEventManager FindEventManagerRecursively(Transform trans) {
        if (trans.parent == null)
        {
            return trans.GetComponent<EnemyEventManager>();
        }
        else {
            return FindEventManagerRecursively(trans.parent);
        }
    }

    public void ReceiveDamage(float dmg) {
        if (enemyEventManager != null) 
        {
            enemyEventManager.CallOnDamage(dmg * damageMultiplier);
            _blood.gameObject.SetActive(true);
        }
    }
}

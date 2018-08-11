using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public LayerMask damagingLayers;
    public float damage;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "shield")
        {
            Debug.Log("it is a shield");  // the fire will be disable by the animation event
            Destroy(gameObject);
        }
        if (IsDamagableLayer(collision.gameObject.layer))
        {
            
            EnemyDamageReceiver receiver = collision.gameObject.GetComponent<EnemyDamageReceiver>();
            if (receiver != null && GameManager.instance._isEnemyDead == false) 
            {
                receiver.ReceiveDamage(damage);
                Destroy(gameObject);
            }
        }
    }

    private bool IsDamagableLayer(int layer) 
    {
        return damagingLayers == (damagingLayers|(1<<layer));
    }
}


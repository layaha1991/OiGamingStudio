using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public LayerMask damagingLayers;
    public float damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsDamagableLayer(collision.gameObject.layer)) {
            EnemyDamageReceiver receiver = collision.gameObject.GetComponent<EnemyDamageReceiver>();
            if (receiver != null) {
                receiver.ReceiveDamage(damage);
                Destroy(gameObject);
            }
        }
    }

    private bool IsDamagableLayer(int layer) {
        return damagingLayers == (damagingLayers|(1<<layer));
    }
}


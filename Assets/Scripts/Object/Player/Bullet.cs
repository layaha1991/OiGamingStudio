using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public LayerMask damagingLayers;
    public float damage;
    public Vector3 velocity;
    public float timeScale=1f;
    public string owner = "Nobody";
    public float destroyBoundX = 40f;
    public float destroyBoundY = 30f;
    private void Start()
    {
        Destroy(gameObject, 10f);
    }
    private void Update()
    {
        transform.Translate(velocity*Time.deltaTime*timeScale,Space.World);
        if (transform.position.x>destroyBoundX || transform.position.x < -destroyBoundX) {
            Destroy(gameObject);
            return;
        }
        if (transform.position.y > destroyBoundY || transform.position.y < -destroyBoundY) {

        }
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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_Enemy_Normal : MonoBehaviour {

    private GameObject player;

    public float speed;
    public float dmg;

	void Start () 
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
       
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.Translate (player.transform.position *speed);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision happened");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hit player");
            PlayerStatusManager playerStatusManager = collision.gameObject.GetComponent<PlayerStatusManager>();
            if (playerStatusManager != null)
            {
                Debug.Log("Decrease HP");
                playerStatusManager.currentHealth -= dmg;
                Destroy(gameObject);
            }
            else if (collision.gameObject.tag == "Parry")
            {
                Debug.Log("Parry");
                Destroy(gameObject);
            }
        }
    }
}

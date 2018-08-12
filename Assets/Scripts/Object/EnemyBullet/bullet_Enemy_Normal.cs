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

        if (collision.gameObject.tag == "Player")
        {

            PlayerStatusManager playerStatusManager = collision.gameObject.GetComponent<PlayerStatusManager>();
            if (playerStatusManager != null)
            {
                playerStatusManager.currentHealth -= dmg;
                Destroy(gameObject);
            }
  
        }
        else if (collision.gameObject.tag == "ParryCollider")
        {
            Debug.Log("The bullet is Parried");
            Destroy(gameObject);
        }
    }
}

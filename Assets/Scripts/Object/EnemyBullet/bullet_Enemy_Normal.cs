using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_Enemy_Normal : MonoBehaviour {

    private GameObject player;

    public float speed;

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
}

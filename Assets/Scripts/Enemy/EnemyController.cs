using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private void Awake()
    {
       
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void DestroyEnemyAfterAnimation()
    {
        Destroy(this.gameObject);
    }


}

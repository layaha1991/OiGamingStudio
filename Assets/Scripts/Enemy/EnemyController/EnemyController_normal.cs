using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_normal: MonoBehaviour {

    [SerializeField]
    private GameObject _bullet_enemy_normal;
    [SerializeField]
    private GameObject _bullet_enemy_normalSpawnPos;

    private void Awake()
    {
        EnemyEventManager.OnEnemyTurn += shoot;

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}


    private void DestroyEnemyAfterAnimation()
    {
        Destroy(this.gameObject);
    }

    private void shoot(bool isEnemyReady)
 
    {
        if (isEnemyReady == true)
        {
            Instantiate(_bullet_enemy_normal, _bullet_enemy_normalSpawnPos.transform.position, Quaternion.identity);
            isEnemyReady = false;
        }
    
    }

private void OnDestroy()
    {
        EnemyEventManager.OnEnemyTurn -= shoot;
    }
}

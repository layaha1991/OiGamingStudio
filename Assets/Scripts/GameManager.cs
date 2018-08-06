using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    // float
    public float timeToSpawnEnemy = 3f;

    //Prefab
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject enemyPos;

    // bool
    public bool _isEnemyDead;

    //Event
    public delegate void Event (bool isEnemyDead);
    public static event Event OnEnemyDead;


    public static GameManager instance;

    private void Awake()
    {
        
        OnEnemyDead += SpawnNewEnemy;

        if (instance == null)
        {
            instance = this;
        }
     
        else if (instance != this)
        {
            Destroy(gameObject);
        }
       
    }


    public void CallOnEnemyDead(bool isEnemyDead)
    {
        if (OnEnemyDead != null)
        {
            OnEnemyDead(isEnemyDead);
        }
    }



    private void SpawnNewEnemy(bool isEnemyDead)

    {
        if (isEnemyDead == true)
        {
            StartCoroutine(SpawnEnemyRoutine());
        }
    }
  

    private IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(timeToSpawnEnemy);
        OnSpawn();
        StopCoroutine(SpawnEnemyRoutine());
    }

    private void OnSpawn()
    {
        Instantiate(enemy, enemyPos.transform.position, Quaternion.identity);
    }
 
}

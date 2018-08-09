using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    // float
    public float timeToSpawnEnemy = 3f;

    [Header("Enemy Prefab")]
    [SerializeField]
    private GameObject[] enemy;

    /**[SerializeField]
    private GameObject enemy_normal;
    [SerializeField]
    private GameObject enemy_shield;
    [SerializeField]
    private GameObject enemy_normal_fly;**/

    [SerializeField]
    private GameObject enemyPos;

    // bool
    public bool _isEnemyDead;
    public bool spawnOnceCount;
    //Event
    public delegate void Event (bool isEnemyDead);
    public static event Event OnEnemyDead;

    public delegate void levelUpHandler();
    public static event levelUpHandler OnLevelUp;



    public static GameManager instance;

    public int level;

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

    private void Start()
    {
        //enemy = new GameObject[3];
        level = 1;
    }
    private void Update()
    {
        OnEnemyDead(_isEnemyDead);
    }


    public void CallOnEnemyDead(bool isEnemyDead)
    {
        if (OnEnemyDead != null)
        {
            OnEnemyDead(isEnemyDead);
        }
    }

    public static void CallOnLevelUp()
    {
        if (OnLevelUp != null)
        {
            OnLevelUp();
        }
    }




    private void SpawnNewEnemy(bool isEnemyDead)

    {
        if (isEnemyDead == true && spawnOnceCount == false)
        {
            StartCoroutine(SpawnEnemyRoutine());
            spawnOnceCount = true;
        }
    }
  

    private IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(timeToSpawnEnemy);
        OnSpawn(enemy[level-1]);
        _isEnemyDead = false;
        spawnOnceCount = false;
        StopCoroutine(SpawnEnemyRoutine());
    }

    private void OnSpawn(GameObject enemy)
    {
        Instantiate(enemy, enemyPos.transform.position, Quaternion.identity);
    }


    /**public void GetEnemyStatusManager()
    {
        EnemyStatusManager enemyStatusManager = new EnemyStatusManager();
        enemyStatusManager = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyStatusManager>();
    }**/
}

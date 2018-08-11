using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    [Header("Enemy Prefab")]
    [SerializeField]
    private GameObject[] enemy;
    [SerializeField]
    private GameObject enemyPos;



    //bool
    public bool _isEnemyDead;
    public bool spawnOnceCount;

    //Event
    public delegate void Event(bool isEnemyDead);
    public static event Event OnEnemyDead;
    public delegate void levelUpHandler(int level);
    public static event levelUpHandler OnLevelUp;


    // Numbers
    public float timeToSpawnEnemy = 3f;
    public int level;

    //GameState
    private EnumGameState gameState;
  
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

    private void Start()
    {
        gameState = EnumGameState.Main;

        level = 1;
        _isEnemyDead = true;
    }

    private void Update()
    {
        OnEnemyDead(_isEnemyDead);
    }

    #region Event
    public void CallOnEnemyDead(bool isEnemyDead)
    {
        if (OnEnemyDead != null)
        {
            OnEnemyDead(isEnemyDead);
        }
    }

    public void CallOnLevelUp(int level)
    {
        if (OnLevelUp != null)
        {
            OnLevelUp(level);
        }
    }
    #endregion



    #region Enemy Spawing
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
        OnSpawn(enemy[level - 1]);
        _isEnemyDead = false;
        spawnOnceCount = false;
        StopCoroutine(SpawnEnemyRoutine());
    }

    private void OnSpawn(GameObject enemy)
    {
        Instantiate(enemy, enemyPos.transform.position, Quaternion.identity);
    }


    #endregion

    #region Enemy Attack

    private void FindEnemy()
    {
        if(GameObject.FindGameObjectWithTag("Enemy")!=null)
        {
            
         
        }
    }
#endregion
    /**public void GetEnemyStatusManager()
    {
        EnemyStatusManager enemyStatusManager = new EnemyStatusManager();
        enemyStatusManager = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyStatusManager>();
    }**/
}

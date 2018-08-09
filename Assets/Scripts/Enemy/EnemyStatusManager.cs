using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusManager : MonoBehaviour
{

    //Stats
    public float maxHealth = 100;
    public float currentHealth = 100;
    public float attackPower = 10;



    //bool




    //prefab


    //Animator
    private Animator enemyAnimator;


    private void Awake()
    {
        EnemyEventManager.OnDamage += OnHit;
    }

    private void Start()
    {
        enemyAnimator = gameObject.GetComponent<Animator>();
    }
    private void checkEnemyDead()
    {
        if (currentHealth <= 0 )
        {
            GameManager.instance._isEnemyDead = true;
        }
        else
        {
            GameManager.instance._isEnemyDead = false;
        }

    }

    private void OnHit(float dmg)
    {
        currentHealth -= dmg;
        checkEnemyDead();
        if (GameManager.instance._isEnemyDead == true)
        {
            if(enemyAnimator != null)
            {
                enemyAnimator.Play("EnemyDead");
                GameManager.instance.level++;// destory object at the end of this Animation
            } else 
            {
                return;
            }
             
        } else 
        {
            return;
        }
    }





}

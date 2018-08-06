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
    [SerializeField]
    private Animator FluffyBlueAnimator;


    private void Awake()
    {
        EnemyEventManager.OnDamage += OnHit;
    }

    private void isEnemyDead()
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
        isEnemyDead();
        if (GameManager.instance._isEnemyDead == true)
        {
            FluffyBlueAnimator.Play("EnemyDead"); // destory object at the end of this Animation
        } else 
        {
            return;
        }
    }



   

}

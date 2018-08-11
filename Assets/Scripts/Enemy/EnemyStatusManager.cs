using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusManager : MonoBehaviour
{

    //Stats
    public float maxHealth = 100;
    public float currentHealth = 100;
    public float attackPower = 10;

    private Transform _blood;

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
        _blood = this.gameObject.transform.GetChild(0);
    }
   

    private void OnHit(float dmg)
    {
        currentHealth -= dmg;
        OnHitAnimation();
        checkEnemyDead();
        if (GameManager.instance._isEnemyDead == true)
        {
            if(enemyAnimator != null)
            {
                enemyAnimator.Play("EnemyDead");
                GameManager.instance.level++;
                Debug.Log("Level up");
                GameManager.instance.CallOnLevelUp(GameManager.instance.level); // destory object at the end of this Animation
            } else 
            {
                return;
            }
             
        } else 
        {
            return;
        }
    }
    private void checkEnemyDead()
    {
        if (currentHealth <= 0)
        {
            GameManager.instance._isEnemyDead = true;
        }
        else
        {
            GameManager.instance._isEnemyDead = false;
        }

    }
    private void OnHitAnimation()
    {
        if (_blood != null)
        {
            _blood.gameObject.SetActive(true);
            StartCoroutine(TurnRedRoutine());
        }
        else 
        {
            return;
        }

        //color change
        //Spawn animation
    }
    private IEnumerator TurnRedRoutine()
    {
        var sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(255, 0, 0, 255);
        yield return new WaitForSeconds(0.1f);
        sprite.color = new Color(255, 255, 255, 255);
        StopCoroutine(TurnRedRoutine());

    }

 


}

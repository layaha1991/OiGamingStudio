using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamController : MonoBehaviour {
    TurnManager turnManager;
    Animator animator;

    public float maxBulletTrackingTime = 3f;
	// Use this for initialization
	void Start () {
        turnManager = TurnManager.Instance;
        animator = GetComponent<Animator>();
        SetupEvents();
	}

    void SetupEvents() {
        turnManager.PlayerReady.OnStart += FocusPlayer;
        turnManager.PlayerParry.OnStart += FocusPlayer;
        turnManager.PlayerBulletTracking.OnStart += TrackBullets;
        turnManager.EnemyBulletTracking.OnStart += TrackBullets;
        turnManager.EnemyReceiveDamage.OnStart += FocusEnemy;
        turnManager.EnemyReady.OnStart += FocusEnemy;
    }

    void ClearEvents()
    {
        turnManager.PlayerReady.OnStart -= FocusPlayer;
        turnManager.PlayerParry.OnStart -= FocusPlayer;
        turnManager.PlayerBulletTracking.OnStart -= TrackBullets;
        turnManager.EnemyBulletTracking.OnStart -= TrackBullets;
        turnManager.EnemyReceiveDamage.OnStart -= FocusEnemy;
        turnManager.EnemyReady.OnStart -= FocusEnemy;
    }

    private void OnDestroy()
    {
        ClearEvents();    
    }

    void FocusPlayer()
    {
        animator.applyRootMotion = false;
        StopCoroutine("TrackVolleyCenter");
        animator.SetTrigger("PlayerFocused");
    }
    void FocusEnemy()
    {
        animator.applyRootMotion = false;
        StopCoroutine("TrackVolleyCenter");
        animator.SetTrigger("EnemyFocused");
    }
    void Overview() {
        animator.SetTrigger("Overviewing");
    }
    void TrackBullets() {
        animator.SetTrigger("BulletTracking");
        //cease the control from animatior, let script control the camera movements
        animator.applyRootMotion = true;
        List<Bullet> validBullets = FindValidBullets();
        if (validBullets != null && validBullets.Count != 0) {
            StartCoroutine(TrackVolleyCenter(validBullets));
        }
    }
    IEnumerator TrackVolleyCenter(List<Bullet> validBullets) {
        Vector3 target = Vector3.zero;
        float trackingTimeElapsed = 0;
        if (validBullets==null || validBullets.Count == 0) {
            yield break;
        }
        while (true) {
            target = Vector3.zero;
            foreach (Bullet b in validBullets) {
                if (b == null) {
                    //if bullet get destroyed then we skip it.
                    continue;
                }
                target += b.transform.position;
                b.timeScale = 1f;
            }
            for (int i = validBullets.Count - 1; i > -1; i--) {
                if (validBullets[i] == null) {
                    validBullets.RemoveAt(i);
                }
            }
            if (validBullets.Count == 0) {
                turnManager.NextTurn();
                yield break;
            }
            if (trackingTimeElapsed > maxBulletTrackingTime)
            {
                turnManager.NextTurn();
                yield break;
            }
            target /= validBullets.Count;
            target.z = transform.position.z;
            Debug.Log("Updating Camera Position to " + target.ToString());
            transform.position = target;
            if (transform.position.x > 15f )
            {
                turnManager.NextTurn();
                yield break;
            }
            //transform.position = Vector3.Lerp(transform.position,target,Time.deltaTime); 
            trackingTimeElapsed += Time.deltaTime;
            yield return null;
        }
        
    }

    List<Bullet> FindValidBullets() {
        Collider2D[] bulletColliders = Physics2D.OverlapCircleAll(Vector2.zero,30f,LayerMask.GetMask("Bullet"));
        if (bulletColliders==null||bulletColliders.Length==0) {
            Debug.LogWarning("Cannot find valid bullet.");
            return null;
        }
        List<Bullet> result = new List<Bullet>();
        foreach (Collider2D c in bulletColliders) {
            Bullet bullet = c.GetComponent<Bullet>();
            if (bullet!=null) {
                if (bullet.transform.position.y < 10f || bullet.transform.position.y > -10f) {
                    result.Add(bullet);
                }
            }
        }
        return result;
    }
	// Update is called once per frame
	void Update () {
		
	}
}

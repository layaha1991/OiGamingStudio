using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class PlayerBulletsBufferingLine : MonoBehaviour {
    [HideInInspector]
    public static float x;
    [Range(1f, 10f)]
    public float slowDownRate = 3f;
    [Range(0f, 1f)]
    public float targetTimeScale = 0.05f;

    Collider2D collider;
    private void Start()
    {
        collider = GetComponent<Collider2D>();
        x = transform.position.x;
        if (Application.isPlaying)
        {
            RegisterEvents();
        }
    }
    private void OnDestroy()
    {
        if (Application.isPlaying)
        {
            RemoveEvents();
        }
    }
    void RegisterEvents() {
        TurnManager turnManager= TurnManager.Instance;
        turnManager.PlayerShoot.OnStart += EnableBuffering;
        turnManager.PlayerBulletTracking.OnStart += DisableBuffering;
    }
    void RemoveEvents() {
        TurnManager turnManager = TurnManager.Instance;
        turnManager.PlayerShoot.OnStart -= EnableBuffering;
        turnManager.PlayerBulletTracking.OnStart -= DisableBuffering;
    }

    void EnableBuffering() {
        collider.enabled = true;
    }
    void DisableBuffering() {
        collider.enabled = false;
        StopCoroutine("SlowDownBullet");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(.2f,1f,.3f,.2f);
        Gizmos.DrawCube(transform.position,new Vector3(.5f,100f,0f));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision from player bullet buffer detected.");
        Bullet bullet = other.GetComponent<Bullet>();
        if (bullet != null && bullet.owner == "Player")
        {
            StartCoroutine(SlowDownBullet(bullet));
        }
    }

    IEnumerator SlowDownBullet(Bullet bullet)
    {
        while (bullet.timeScale > targetTimeScale)
        {
            bullet.timeScale -= Time.deltaTime * slowDownRate;
            yield return null;
        }
        bullet.timeScale = targetTimeScale;
        bullet.timeScale = 0.05f;
    }
}

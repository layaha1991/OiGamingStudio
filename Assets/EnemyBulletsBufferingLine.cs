using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletsBufferingLine : MonoBehaviour {
    [HideInInspector]
    public static float x;
    [Range(1f,10f)]
    public float slowDownRate=3f;
    [Range(0f,1f)]
    public float targetTimeScale = 0.05f;
    Collider2D collider;

	void Start () {
        x = transform.position.x;
        collider = GetComponent<Collider2D>();
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
    void RegisterEvents()
    {
        TurnManager turnManager = TurnManager.Instance;
        turnManager.PlayerShoot.OnStart += EnableCollider;
        turnManager.PlayerBulletTracking.OnStart += DisableCollider;
    }
    void RemoveEvents()
    {
        TurnManager turnManager = TurnManager.Instance;
        turnManager.PlayerShoot.OnStart -= EnableCollider;
        turnManager.PlayerBulletTracking.OnStart -= DisableCollider;
    }

    void EnableCollider()
    {
        collider.enabled = true;
    }
    void DisableCollider()
    {
        collider.enabled = false;
        StopCoroutine("SlowDownBullet");
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, .2f, .3f, .2f);
        Gizmos.DrawCube(transform.position, new Vector3(.5f, 100f, 0f));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Bullet bullet = other.GetComponent<Bullet>();
        if (bullet != null && bullet.owner=="Enemy") {
            StartCoroutine(SlowDownBullet(bullet));
        }
    }

    IEnumerator SlowDownBullet(Bullet bullet) {
        while (bullet.timeScale > targetTimeScale) {
            bullet.timeScale -= Time.deltaTime * slowDownRate;
            yield return null;
        }
        bullet.timeScale = targetTimeScale;
        bullet.timeScale = 0.05f;
    }
}

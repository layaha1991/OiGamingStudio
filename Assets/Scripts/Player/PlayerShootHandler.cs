using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootHandler : MonoBehaviour {
    [SerializeField]
    private GunStates gunStates = GunStates.Stopped;
    private PlayerStatusManager playerStatusManager;
    public int maxAmmo;
    public int currentAmmo;
    public Transform GunLazer;
    public GameObject BulletPrefab;
    //Shoot Direction is the gun forward

    Quaternion targetRot;
    Quaternion currentRot;
    public float rotateSpeed = 1;
    public Vector3 StoppedEulerangle;
    public Vector3 ReadyEulerangle;
    public float shootSpeed = 20f;

    TurnManager turnManager;
    public bool ableToShoot = false;
    public bool ableToParry = false;
    [Space]
    [Header("Turn settings")]
    public float postTurnWaitPeriod = 1f;
    private void Awake()
    {
        playerStatusManager = GetComponent<PlayerStatusManager>();
        PlayerEventManager.OnTap += HandleTap;
        PlayerEventManager.OnParry += HandleParry;
    }
    private void Start()
    {
        turnManager = TurnManager.Instance;
        turnManager.PlayerShoot.OnStart += OnPlayerShootStartHandler;
        turnManager.PlayerShoot.OnFinish += OnPlayerShootFinishHandler;
    }

    void OnPlayerShootStartHandler() {
        ableToShoot = true;
        StopCoroutine(SwingShootingLazerCoroutine());
        StartCoroutine(SwingShootingLazerCoroutine());
    }
    void OnPlayerShootFinishHandler()
    {
        ableToShoot = false;
        StopCoroutine(SwingShootingLazerCoroutine());
    }

    private void HandleTap(Vector2 tapPos) {
        if (ableToShoot) {
            if (currentAmmo>0) {
                currentAmmo--;
                Bullet bullet = Instantiate(BulletPrefab,GunLazer.position,GunLazer.rotation).GetComponent<Bullet>();
                bullet.velocity = GunLazer.right*shootSpeed;
                bullet.damage *= playerStatusManager.attackPower;
                bullet.owner = "Player";
            }
        }
    }
    private void CheckAbleToParry()
    {
        if (playerStatusManager.Energy >= 10f)
        {
            ableToParry = true;

        } else
        {
            ableToParry = false;

        }
    }
    private void HandleParry()
    {
        CheckAbleToParry();
        if(ableToParry == true)
        {
            playerStatusManager.Energy -= 10f;
            Transform parryCollider = transform.Find("ParryCollider");
            parryCollider.gameObject.SetActive(true);
        }
    }
    IEnumerator SwingShootingLazerCoroutine() {
        gunStates = GunStates.Rotating;
        GunLazer.rotation = Quaternion.Euler(ReadyEulerangle);
        GunLazer.gameObject.SetActive(true);
        float lerpValue = 0;
        while (gunStates!=GunStates.Stopped) 
        {
            lerpValue += Time.deltaTime * rotateSpeed;
            switch (gunStates) {
                case GunStates.Rotating:
                    GunLazer.rotation = Quaternion.Lerp(Quaternion.Euler(ReadyEulerangle), Quaternion.Euler(StoppedEulerangle), lerpValue);
                    if (Quaternion.Angle(GunLazer.rotation, Quaternion.Euler(StoppedEulerangle))<0.5f) {
                        gunStates = GunStates.Stopped;
                    }
                    break;
            }
            yield return null;
        }
        yield return new WaitForSeconds(postTurnWaitPeriod);
        GunLazer.gameObject.SetActive(false);
        if (turnManager.CurrentTurn == turnManager.PlayerShoot) 
        {
            turnManager.NextTurn();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(GunLazer.position, GunLazer.position + GunLazer.right*10f);
    }

    public enum GunStates { Rotating,Stopped }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerShootHandler : MonoBehaviour {
    [SerializeField]
    private GunStates gunStates = GunStates.Stopped;
    private PlayerStatusManager playerStatusManager;
    public int maxAmmo;
    public int currentAmmo;
    public Transform Gun;
    public Transform GunPivot;
    public GameObject BulletPrefab;
    //Shoot Direction is the gun forward

    Quaternion targetRot;
    Quaternion currentRot;
    public float rotateSpeed = 1;
    public Vector3 StoppedEulerangle;
    public Vector3 ReadyEulerangle;
    public float shootSpeed = 10f;

    TurnManager turnManager;

    public bool ableToShoot = false;
    private void Awake()
    {
        playerStatusManager = GetComponent<PlayerStatusManager>();
        PlayerEventManager.OnTap += HandleTap;
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
                GameObject bullet = Instantiate(BulletPrefab,Gun.position,Gun.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = Gun.right*shootSpeed;
                bullet.GetComponent<Bullet>().damage *= playerStatusManager.attackPower;
            }
        }
    }

    IEnumerator SwingShootingLazerCoroutine() {
        gunStates = GunStates.Rotating;
        Gun.rotation = Quaternion.Euler(ReadyEulerangle);
        float lerpValue = 0;
        while (gunStates!=GunStates.Stopped) 
        {
            lerpValue += Time.deltaTime * rotateSpeed;
            switch (gunStates) {
                case GunStates.Rotating:
                    Gun.rotation = Quaternion.Lerp(Quaternion.Euler(ReadyEulerangle), Quaternion.Euler(StoppedEulerangle), lerpValue);
                    if (Quaternion.Angle(Gun.rotation, Quaternion.Euler(StoppedEulerangle))<0.5f) {
                        gunStates = GunStates.Stopped;
                    }
                    break;
            }
            yield return null;
        }
        if (turnManager.CurrentTurn == turnManager.PlayerShoot) 
        {
            turnManager.NextTurn();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Gun.position, Gun.position + Gun.right*10f);
    }

    public enum GunStates { Rotating,Stopped }
}

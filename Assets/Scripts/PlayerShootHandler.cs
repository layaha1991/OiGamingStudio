using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class PlayerShootHandler : MonoBehaviour {
    [SerializeField]
    private GunStates gunStates = GunStates.Ready;
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
    private float readyTime = 1.5f;
    private float stopTime = 1.5f;
    private float resetTime =2f;

    public float shootSpeed = 10f;
    private void Awake()
    {
        playerStatusManager = GetComponent<PlayerStatusManager>();
        PlayerEventManager.OnTap += HandleTap;
    }
    private void Start()
    {
        StartCoroutine(SwingShootingLazerCoroutine());
    }
    private void HandleTap(Vector2 tapPos) {
        if (gunStates == GunStates.Rotating) {
            if (currentAmmo>0) {
                currentAmmo--;
                GameObject bullet = Instantiate(BulletPrefab,Gun.position,Gun.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = Gun.right*shootSpeed;
                bullet.GetComponent<Bullet>().damage *= playerStatusManager.attackPower;
            }
        }
    }

    IEnumerator SwingShootingLazerCoroutine() {
        while (true) {
            switch (gunStates) {
                case GunStates.Ready:
                    yield return new WaitForSeconds(readyTime);
                    gunStates = GunStates.Rotating;
                    break;
                case GunStates.Rotating:
                    Gun.rotation = Quaternion.Slerp(Gun.rotation, Quaternion.Euler(StoppedEulerangle), Time.deltaTime * rotateSpeed);
                    if (Quaternion.Angle(Gun.rotation, Quaternion.Euler(StoppedEulerangle))<0.5f) {
                        gunStates = GunStates.Stopped;
                    }
                    break;
                case GunStates.Stopped:
                    yield return new WaitForSeconds(stopTime);
                    gunStates = GunStates.Reseting;
                    break;
                case GunStates.Reseting:
                    float elapsedTime = 0;
                    while (elapsedTime < resetTime) {
                        elapsedTime += Time.deltaTime;
                        Gun.rotation = Quaternion.Slerp(Quaternion.Euler(StoppedEulerangle), Quaternion.Euler(ReadyEulerangle), elapsedTime / resetTime);
                        yield return null;
                    }
                    gunStates = GunStates.Rotating;
                    break;
            }
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Gun.position, Gun.position + Gun.right*10f);
    }

    public enum GunStates { Reseting,Rotating,Stopped,Ready }
}

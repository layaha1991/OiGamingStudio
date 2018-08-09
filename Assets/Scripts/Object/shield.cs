using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shield : MonoBehaviour {

    [SerializeField]
    private GameObject spark;
    [SerializeField]
    private GameObject sparkPos;

    private void OnTriggerEnter2D (Collider2D other)
    {
        Debug.Log("detected Enter 2D");
        if (other.gameObject.tag == "Bullet")
        {
            Debug.Log("instantiate spark");
            Instantiate(spark, sparkPos.transform.position, Quaternion.identity);
        }
    }
}

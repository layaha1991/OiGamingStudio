using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_shield : MonoBehaviour {


        [SerializeField]
        private GameObject _bullet_enemy_shield;
        [SerializeField]
        private GameObject _bullet_enemy_shieldSpawnPos;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        private void DestroyEnemyAfterAnimation()
        {
            Destroy(this.gameObject);
        }

        private void shoot()

        {
            Instantiate(_bullet_enemy_shield, _bullet_enemy_shieldSpawnPos.transform.position, Quaternion.identity);
        }

    }

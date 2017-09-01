using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider col)
    {
        Destroy(gameObject);
        if (col.gameObject.CompareTag("Enemy"))
        {
            col.gameObject.GetComponent<EnemyBehavior>().Hit();
        }
        else if (col.gameObject.CompareTag("Boss"))
        {
            col.gameObject.GetComponent<BossBehavior>().Hit();
        }
        else if (col.gameObject.CompareTag("Player"))
        {
            Director.HeroDied();
        }
        else if (col.gameObject.CompareTag("Coin"))
        {
            col.gameObject.GetComponent<CoinBehaviour>().Hit();
        }
    }
}


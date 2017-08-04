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
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<EnemyBehavior>().Hit();
        }
        else if (col.gameObject.tag == "Boss")
        {
            col.gameObject.GetComponent<BossBehavior>().Hit();
        }
    }
}


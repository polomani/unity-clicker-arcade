using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnsBehaviour : MonoBehaviour {

    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    public float spawnSpeed = 0.5f;
    private GameObject[] spawnPoints;

	void Start () {
        Director.Spawns = this;
        spawnPoints = GameObject.FindGameObjectsWithTag("Respawn").
            Where(o => o.GetComponent<SpawnPlaceBehavior>().spawnType!=SpawnType.CENTER).
            ToArray();
        StartCoroutine("AddEnemy");
	}

    private IEnumerator AddEnemy() {
        while (true)
        {
            if (!Director.BossMode && Director.NeedMoreEnemies())
            {
                GameObject spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
                GameObject enemy = Instantiate(enemyPrefab, spawn.transform.position, Quaternion.Euler(0, 0, 0));
                enemy.GetComponent<EnemyBehavior>().spawnType = spawn.GetComponent<SpawnPlaceBehavior>().spawnType;
                Director.EnemiesSummoned++;
                yield return new WaitForSeconds(spawnSpeed);
            }
            else
            {
                //StopCoroutine("AddEnemy");
                yield return null;
            }
        }
    }

    public void SummonBoss ()
    {
        Instantiate(bossPrefab);
        StopCoroutine("AddEnemy");
    }
}

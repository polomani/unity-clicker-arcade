using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossBehavior : MonoBehaviour {

    public float bulletSpeed = 10;
    public GameObject bulletPrefab;
    private GameObject spawn;
    private MoveType lastMove;
    private int movesInRow = 1;
    private SpawnType spawnType;
    private GameObject[] spawnPoints;
    private float health;
    private const float hitPower = 10;
    public float dodgeSpeed = 10;

    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    void Awake()
    {
        health = 100;
        spawnPoints = GameObject.FindGameObjectsWithTag("Respawn").
            Where(o => o.GetComponent<SpawnPlaceBehavior>().spawnType != SpawnType.DOWN).
            ToArray();
        GameObject[] firstSpawns = GameObject.FindGameObjectsWithTag("Respawn").
            Where(o => o.GetComponent<SpawnPlaceBehavior>().spawnType == SpawnType.TOP).
            ToArray();
        spawn = firstSpawns[Random.Range(0, firstSpawns.Length)];
        transform.position = spawn.transform.position;
        spawnType = spawn.GetComponent<SpawnPlaceBehavior>().spawnType;
        StartCoroutine("PrepareForBattle");
	}



    public void Hit()
    {
        health -= hitPower;
        if (health == 0)
        {
            Destroy(gameObject);
            Director.BossKilled();
        }     
    }
	
	void Update () {
        transform.position = Vector3.Lerp(transform.position, spawn.transform.position, 1/dodgeSpeed);
	}

    private IEnumerator PrepareForBattle()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine("AI");
    }

    private IEnumerator AI()
    {     
        while (true)
        {
            MoveType move = DecideMove();
            if (move == MoveType.DODGE) Dodge();
            else Fire();
            yield return new WaitForSeconds(1);
        }
    }

    private void Dodge()
    {
        GameObject[] otherSpawns = spawn.GetComponent<SpawnPlaceBehavior>().nearestSpawnPlaces.ToArray();
        GameObject newSpawn =  otherSpawns [Random.Range(0, otherSpawns.Length)];
        spawn = newSpawn;    
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, 0));
        Vector3 hero = Director.Hero.transform.position;
        Vector3 gun = transform.position;
        gun.z = hero.z;
        bullet.GetComponent<Rigidbody>().velocity = (hero - gun).normalized * bulletSpeed;
    } 

    private MoveType DecideMove() {
        MoveType move =  (MoveType) Random.Range(0, 2);
        if (move == MoveType.DODGE && movesInRow == 3)
        {
            move = MoveType.FIRE;
            movesInRow = 1;
        } 
        else if (move == MoveType.FIRE && movesInRow == 2)
        {
            move = MoveType.DODGE;
            movesInRow = 1;
        }
        else if (move!=lastMove)
        {
            movesInRow = 1;
        }
        else
        {
            movesInRow++;
        }
        return lastMove = move;
    }
}

enum MoveType {
    DODGE, FIRE
}

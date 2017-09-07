using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

    public float speed;
    public float highSpeed;
    public float slowSpeed;
    private Rigidbody rBody;
    private SpawnType spawnType;
    public SpawnPlaceBehavior spawn;
    private EnemyType type;
    private bool isGrounded = true;
    private EnemyState state = EnemyState.Init;
    private Vector3 xSide;
    public GameObject spinner;



	void Start () {
        spawnType = spawn.spawnType;
        rBody = GetComponent<Rigidbody>();
        xSide = new Vector3 (-transform.position.normalized.x, 0, 0);
        if (spawnType == SpawnType.TOP)
        {
            type = Random.Range (0,2)==0 ? EnemyType.TopStrongJumper : EnemyType.TopWeakJumper;
            state = EnemyState.UpperRun;
            Vector3 moveVector = xSide * speed;
            rBody.velocity = moveVector;
        }
        else
        {
            rBody.useGravity = false;
            state = EnemyState.CrowlUp;
        }
	}

    void OnCollisionEnter(Collision col)
    {
        if (state == EnemyState.Jump)
        {
            Vector3 v = xSide * speed;
            if (spawnType == SpawnType.TOP) v *= -1;
            rBody.velocity = v;
        }
    }

    void OnCollisionStay(Collision col)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision col)
    {
        isGrounded = false;
    }

    void Update()
    {
        spinner.transform.Rotate(new Vector3(0, 0, 1), 5);
        if (!isGrounded && state==EnemyState.UpperRun)
        {
            state = EnemyState.Jump;
            Vector3 moveVector = (type == EnemyType.TopStrongJumper) ?
                                 xSide * highSpeed : xSide * slowSpeed;
            rBody.velocity = moveVector;
        }
        if (state == EnemyState.CrowlUp)
        {
            Vector3 upperSpawn = spawn.nearestSpawnPlaces[0].transform.position;
            if (upperSpawn.y > rBody.transform.position.y)
            {     
                rBody.velocity = new Vector3(0, speed, 0);
            }
            else
            {
                rBody.useGravity = true;
                state = EnemyState.CrowlAhead;
            }     
        } else if (state == EnemyState.CrowlAhead)
        {
            Vector3 upperSpawn = spawn.nearestSpawnPlaces[0].transform.position;
            if (rBody.position.z > upperSpawn.z - 0.2)
            {
                rBody.velocity = new Vector3(0, 0, -speed);
            }
            else
            {
                state = EnemyState.Jump;
                rBody.velocity = new Vector3();
            } 
        }
    }

    public void Hit()
    {
        Destroy(gameObject);
        Director.EnemyKilled();
        Director.Score++;
    }
}

enum EnemyState {
    Init, UpperRun, Jump, LowerRun, CrowlUp, CrowlAhead
}

enum EnemyType {
    TopWeakJumper, TopStrongJumper, Down
}
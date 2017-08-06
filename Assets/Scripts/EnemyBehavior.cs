using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

    public float speed = 20;
    private Rigidbody rBody;
    public SpawnType spawnType;
    private EnemyType type;
    private bool isGrounded = true;
    private EnemyState state = EnemyState.Init;
    private Vector3 xSide;



	void Start () {
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
        if (!isGrounded && state==EnemyState.UpperRun)
        {
            state = EnemyState.Jump;
            Vector3 moveVector = (type == EnemyType.TopStrongJumper) ?
                                 xSide * 8 / 1.5f : xSide * 8 / 6f;
            rBody.velocity = moveVector;
        }
        if (state == EnemyState.CrowlUp)
        {
            if (isGrounded)
            {
                rBody.velocity = new Vector3(0, speed, 0);
            }
            else
            {
                state = EnemyState.CrowlAhead;
            }     
        }
        if (state == EnemyState.CrowlAhead)
        {
            if (rBody.position.z > 0 || isGrounded)
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
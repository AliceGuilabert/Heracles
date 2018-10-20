using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour {

    public float moveSpeed;
    private bool moveRight;

    Rigidbody2D myRigid;
    PlayerController player;
    private Animator myAnim;
    EnemyHealthManager myHealth;

    public Transform wallCheck;
    private float wallCheckRadius;
    public LayerMask whatIswall;
    private bool hittingWall;
    private bool atEdge;
    public Transform edgeCheck;

    public float attackRange;
    private bool isAttacking;

    // Use this for initialization
    void Start () {
        myRigid = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>();
        myHealth = gameObject.GetComponent<EnemyHealthManager>();
        isAttacking = false;
        wallCheckRadius = 0.1f;
        myAnim = GetComponent<Animator>();
	}

    private void FixedUpdate()
    {
        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIswall);
        atEdge = Physics2D.OverlapCircle(edgeCheck.position, wallCheckRadius, whatIswall);
    }
    // Update is called once per frame
    void Update () {

        if (myHealth.alive)
        {

            float distancePlayer = player.transform.position.x - transform.position.x;
            if (Mathf.Abs(distancePlayer) < attackRange)
            {
                isAttacking = true;
            }
            else
            {
                isAttacking = false;
            }

            if (!isAttacking)
            {
                //Debug.Log("hitting wall" + hittingWall);
                //Debug.Log("on edge ?" + atEdge);
                if (hittingWall || !atEdge)
                {
                    moveRight = !moveRight;
                }

                if (moveRight)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                    myRigid.velocity = new Vector2(moveSpeed, myRigid.velocity.y);
                }
                else
                {
                    transform.localScale = new Vector3(1f, 1f, 1f);
                    myRigid.velocity = new Vector2(-moveSpeed, myRigid.velocity.y);
                }
            }

            else
            {
                if (distancePlayer > 0)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                    if (!atEdge)
                    {
                        myRigid.velocity = Vector2.zero;
                    } else
                    {
                        myRigid.velocity = new Vector2(moveSpeed, myRigid.velocity.y);
                    }
                }
                else if (distancePlayer < 0)
                {
                    transform.localScale = new Vector3(1f, 1f, 1f);
                    if (!atEdge)
                    {
                        myRigid.velocity = Vector2.zero;
                    }
                    else
                    {
                        myRigid.velocity = new Vector2(-moveSpeed, myRigid.velocity.y);
                    }
                }         
            }
            myAnim.SetFloat("distance", distancePlayer);

        }
    }
}

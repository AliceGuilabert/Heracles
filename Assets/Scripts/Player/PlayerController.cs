using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float jumpHeight;
    public float timeBetweenAttacks;
    private float timeSpendBetweenAttack;

    private Rigidbody2D myRigid;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool onTheGround;

    private Animator myAnim;

    public Inventory myInvent;
    public bool pause;

    private void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        timeSpendBetweenAttack = timeBetweenAttacks;
        pause = false;

    }

    private void FixedUpdate()
    {
        onTheGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void Update()
    {
        if (!pause)
        {
            myAnim.SetBool("OnTheGround", onTheGround);

            if (Input.GetKeyDown(KeyCode.Space) && timeSpendBetweenAttack >= timeBetweenAttacks)
            {
                myAnim.SetTrigger("Attack");
                timeSpendBetweenAttack = 0;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) && onTheGround)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(myRigid.velocity.x, jumpHeight);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                myRigid.velocity = new Vector2(moveSpeed, myRigid.velocity.y);
                transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                myRigid.velocity = new Vector2(-moveSpeed, myRigid.velocity.y);
                transform.localScale = new Vector3(-1.3f, 1.3f, 1.3f);
            }

            //Open Inventory
            if (Input.GetKeyDown(KeyCode.Return))
            {
                myInvent.gameObject.SetActive(true);
                myInvent.Open();
                pause = true;
            }

            if (!Input.anyKey)
            {
                myRigid.velocity = new Vector2(0, myRigid.velocity.y);
            }

            myAnim.SetFloat("Jump", myRigid.velocity.y);
            myAnim.SetFloat("Speed", Mathf.Abs(myRigid.velocity.x));
            timeSpendBetweenAttack++;
        }
        else
        {
            myRigid.velocity = new Vector2(0, 0);
            myAnim.SetFloat("Speed", Mathf.Abs(0));
            if (Input.GetKeyDown(KeyCode.Return) && myInvent.gameObject.activeSelf)
            {
                myInvent.Close();
                myInvent.gameObject.SetActive(false);
                pause = false;
            }
        }
    }
}

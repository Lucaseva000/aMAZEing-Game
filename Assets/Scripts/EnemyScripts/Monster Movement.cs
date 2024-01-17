using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{

    public Transform[] checkpoints;
    public float movespeed;
    private int checkpointDestination = 0;

    public Transform playerTransform;
    public Rigidbody2D enemyRB;
    private bool isChasing;
    public float chaseDistance;
<<<<<<< HEAD
<<<<<<< HEAD
    public float chaseLose;
    
=======
=======
    public float totalChaseTime = 5;
    private float currentChaseTime;
>>>>>>> 6cc97892325b65bfb0937aee1b79fc7ecd30c275

    [Header("KnockBack")]
    [SerializeField] public float KBForce;
    [SerializeField] public float KBCounter;
    public float KBTotalTime;
    public bool KnockFromRight;

<<<<<<< HEAD
>>>>>>> 222310568f4f8a5164a5be8b1f6d61a456ba86fa
=======
>>>>>>> 6cc97892325b65bfb0937aee1b79fc7ecd30c275

    // Update is called once per frame
    void FixedUpdate()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        if(KBCounter <= 0)
<<<<<<< HEAD
        {
            movement();
        }
        else
        {
            if(KBCounter == KBTotalTime)
            {
                if (KnockFromRight)
                {
                    enemyRB.velocity = new Vector2(-KBForce, (KBForce/2));
                }
                else if (!KnockFromRight)
                {
                    enemyRB.velocity = new Vector2(KBForce, (KBForce/2));
                }
            }
            KBCounter -= Time.deltaTime;
        }
        
    }

    public void movement()
    {
        if (isChasing)
        {
=======
        {
            movement();
        }
        else
        {
            if(KBCounter == KBTotalTime)
            {
                if (KnockFromRight)
                {
                    enemyRB.velocity = new Vector2(-KBForce, (KBForce/2));
                }
                else if (!KnockFromRight)
                {
                    enemyRB.velocity = new Vector2(KBForce, (KBForce/2));
                }
            }
            KBCounter -= Time.deltaTime;
        }
        
    }

    public void movement()
    {
        if (isChasing && currentChaseTime > 0)
        {
>>>>>>> 6cc97892325b65bfb0937aee1b79fc7ecd30c275
            if (transform.position.x > playerTransform.position.x)
            {
                transform.localScale = new Vector3(2, 2, 1);
                transform.position += Vector3.left * movespeed * 2 * Time.deltaTime;
            }
            if (transform.position.x < playerTransform.position.x)
            {
                transform.localScale = new Vector3(-2, 2, 1);
                transform.position += Vector3.right * movespeed * 2 * Time.deltaTime;
            }

            currentChaseTime -= Time.deltaTime;

        }
        else
        {
<<<<<<< HEAD
=======
            isChasing = false;
            currentChaseTime = totalChaseTime;

>>>>>>> 6cc97892325b65bfb0937aee1b79fc7ecd30c275
            if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
            {
                isChasing = true;
            }



            if (checkpointDestination == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, checkpoints[0].position, movespeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, checkpoints[0].position) < .2f)
                {
                    transform.localScale = new Vector3(-2, 2, 1);
                    checkpointDestination = 1;
                }
            }

            if (checkpointDestination == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, checkpoints[1].position, movespeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, checkpoints[1].position) < .2f)
                {
                    transform.localScale = new Vector3(2, 2, 1);
                    checkpointDestination = 0;
                }
            }

        }
    }
}

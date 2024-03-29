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
    public float chaseLose;
    public float totalChaseTime = 5;
    private float currentChaseTime;
    [Header("KnockBack")]
    [SerializeField] public float KBForce;
    [SerializeField] public float KBCounter;
    public float KBTotalTime;
    public bool KnockFromRight;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // Flips enemy
        spriteRenderer.flipX = enemyRB.velocity.x < 0f;

        // Causes Knockback and disables movement when hit
        playerTransform = GameObject.FindWithTag("Player").transform;
        if(KBCounter <= 0)

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

            if (transform.position.x > playerTransform.position.x)
            {
                //transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1);
                transform.position += Vector3.left * movespeed * 2 * Time.deltaTime;
            }
            if (transform.position.x < playerTransform.position.x)
            {
                //transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
                transform.position += Vector3.right * movespeed * 2 * Time.deltaTime;
            }

            currentChaseTime -= Time.deltaTime;

        }
        else
        {
            isChasing = false;
            currentChaseTime = totalChaseTime;

            if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
            {
                isChasing = true;
            }



            if (checkpointDestination == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, checkpoints[0].position, movespeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, checkpoints[0].position) < .2f)
                {
                    //transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
                    checkpointDestination = 1;
                }
            }

            if (checkpointDestination == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, checkpoints[1].position, movespeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, checkpoints[1].position) < .2f)
                {
                    //transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
                    checkpointDestination = 0;
                }
            }

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{

    public Transform[] checkpoints;
    public float movespeed;
    private int checkpointDestination;

    public Transform playerTransform;
    private bool isChasing;
    public float chaseDistance;
    

    // Update is called once per frame
    void Update()
    {
        if (isChasing)
        {
            if(transform.position.x > playerTransform.position.x)
            {
                transform.localScale = new Vector3(2, 2, 1);
                transform.position += Vector3.left * movespeed * 2 *  Time.deltaTime;
            }
            if (transform.position.x < playerTransform.position.x)
            {
                transform.localScale = new Vector3(-2, 2, 1);
                transform.position += Vector3.right * movespeed * 2 * Time.deltaTime;
            }

        }
        else
        {
            if(Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
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

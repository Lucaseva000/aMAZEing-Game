using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EneE : MonoBehaviour
{
    public int damage;
    public PlayerHealth p;


    void OnCollisionEnter2D(Collision2D collisionEnemy)
    {
        if (collisionEnemy.gameObject.CompareTag("Player"))
        {
            p.decreaseHealth(damage);

        }
    }
}

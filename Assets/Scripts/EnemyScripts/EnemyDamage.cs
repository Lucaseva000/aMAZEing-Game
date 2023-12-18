using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage;
    private PlayerController p;
    private AttackArea d;
    public bool isBeingHit;


    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player"))
        {
            d = other.GetComponent<AttackArea>();
            if (!isBeingHit && !d.IfHit())
            {
                p = other.GetComponentInParent<PlayerController>();
                p.KBCounter = p.KBTotalTime;
                if(collision.transform.position.x <= transform.position.x)
                {
                    p.KnockFromRight = true;
                }
                else
                {
                    p.KnockFromRight = false;
                }

                p.damage(damage);
            }
        }
    }
}

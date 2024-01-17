using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class AttackArea : MonoBehaviour
{
    
    public LayerMask enemyLayer;
    public GameObject aimer;
    public int damage = 3;
    private GameObject e;
    private RaycastHit2D r;

   
    public void Attack()
    {
        if(Physics2D.Linecast(transform.position, aimer.transform.position))
        {
            r = Physics2D.Linecast(transform.position, aimer.transform.position);
            
            if (r.transform.gameObject.CompareTag("Enemy"))
            {
                e = r.transform.gameObject;
                e.GetComponent<EnemyHealth>().damage(damage);
                e.GetComponent<MonsterMovement>().KBCounter = e.GetComponent<MonsterMovement>().KBTotalTime;
                if(transform.position.x >= e.transform.position.x)
                {
                    e.GetComponent<MonsterMovement>().KnockFromRight = true;
                }
                else
                {
                    e.GetComponent<MonsterMovement>().KnockFromRight = false;
                }

            }
        }
    }
 
}

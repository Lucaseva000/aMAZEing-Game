using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class AttackArea : MonoBehaviour
{
    
    public LayerMask enemyLayer;
    public GameObject aimer;
    public int damage = 3;
    private EnemyHealth e;
    private RaycastHit2D r;

   
    public void Attack()
    {
        if(Physics2D.Linecast(transform.position, aimer.transform.position))
        {
            r = Physics2D.Linecast(transform.position, aimer.transform.position);
            
            if (r.transform.gameObject.CompareTag("Enemy"))
            {
                e = r.transform.gameObject.GetComponent<EnemyHealth>();
                e.damage(damage);
            }
        }
    }
 
}

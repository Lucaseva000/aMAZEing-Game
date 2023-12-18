using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{

    public int damage = 3;
    private EnemyDamage d;
    private bool didHit = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Enemy")){
            Debug.Log("Player");
            d = other.GetComponent<EnemyDamage>();
            d.isBeingHit = true;
            EnemyHealth e = other.GetComponent<EnemyHealth>();
            e.damage(damage);
            didHit = true;
            
                }
    }
 
    public void setEnemyNormal()
    {
        d.isBeingHit = false;
    }
    public bool IfHit()
    {
        return didHit;
    }
    public void setHit(bool b)
    {
        didHit = b;
    }
}

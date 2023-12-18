using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject AttackArea;
    private bool attacking;
    public float timeToAttack = 0.25f;
    public float timer = 0f;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Attack();
        }

        if (attacking)
        {
            timer += Time.deltaTime;
            if(timer >= timeToAttack)
            {
                timer = 0;
                attacking = false;
                AttackArea.SetActive(attacking);
                if (AttackArea.GetComponent<AttackArea>().IfHit())
                {
                    AttackArea.GetComponent<AttackArea>().setHit(false);
                    AttackArea.GetComponent<AttackArea>().setEnemyNormal();
                    
                }
            }
        }
    }
    private void Attack()
    {
        attacking = true;
        AttackArea.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject AttackArea;
    private bool attacking;
    public float timeToAttack = 0.25f;
    public float timer = 0f;


    // The damage only applies on the first frame of the attack
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !attacking)
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
            }
        }
    }
    private void Attack()
    {
        attacking = true;
        AttackArea.SetActive(true);
        AttackArea.GetComponent<AttackArea>().Attack();
    }
}

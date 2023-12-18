using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyHealth;
    public GameObject enemy;

    public void damage(int dmg)
    {
        enemyHealth -= dmg;
    }

    public void Update()
    {
        if(enemyHealth <= 0)
        {
            Destroy(enemy);
        }
    }
}

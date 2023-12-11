using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerHealth")]
public class PlayerHealth : ScriptableObject
{
    public int maxHealth = 10;
    public int currentHealth;
    public void DecreaseHealth(int a)
    {
        currentHealth -= a;
    }

    public void SetPlayerHealth(int h)
    {
        currentHealth = h;
    }

    public int getHealth()
    {
        return currentHealth;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }
}

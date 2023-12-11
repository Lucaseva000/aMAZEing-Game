using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerHealth")]
public class PlayerHealth : ScriptableObject
{
    public int maxHealth = 10;
    public int currentHealth;
    public bool pauseHealthRegen = false;
    public bool justHit = false;


    public int getHealth()
    {
        return currentHealth;
    }
    public int getMaxHealth()
    {
        return maxHealth;
    }
    public void setHealth(int i)
    {
        currentHealth = i;
    }

    public void decreaseHealth(int i)
    {
        currentHealth -= i;
        justHit = true;


    }

    public void increaseHealth(int i)
    {
        currentHealth += i;
    }

    public bool getRegen()
    {
        return pauseHealthRegen;
    }
    public void setRegenPause(bool b)
    {
        pauseHealthRegen = b;
    }

    public void setJustHit(bool b)
    {
        justHit = b;
    }

    public bool getJustHit()
    {
        return justHit;
    }

}

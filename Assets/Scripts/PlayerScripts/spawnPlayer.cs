using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPlayer : MonoBehaviour
{

    public MoveDirectionSO m;
    public string newDirection;
    public GameObject p;

    //Sets the player based on the direction transfered. Uses SO movedirection 
    void Start()
    {
        if (newDirection.Equals(m.getDirection()))
        {
            p.SetActive(true);
            m.setDirection(null);
        }
    }


}

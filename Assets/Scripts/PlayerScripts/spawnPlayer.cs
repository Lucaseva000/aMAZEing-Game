using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPlayer : MonoBehaviour
{

    public MoveDirectionSO m;
    public string newDirection;
    public GameObject p;

    //Sets the player based on the direction transfered. Uses SO movedirection. If no direction, spawns at last used direction
    void Start()
    {

        if (newDirection.Equals(m.getDirection()))
        {
            p.SetActive(true);
        }
    }


}

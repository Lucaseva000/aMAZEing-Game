using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPlayer : MonoBehaviour
{

    public MoveDirectionSO m;
    public string newDirection;
    public GameObject p;


    void Start()
    {
        if (newDirection.Equals(m.getDirection()))
        {
            p.SetActive(true);
        }
    }


}

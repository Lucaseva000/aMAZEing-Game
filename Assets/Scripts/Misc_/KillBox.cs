using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player"))
        {

            other.GetComponentInParent<PlayerController>().p.setHealth(0);

        }
    }
}

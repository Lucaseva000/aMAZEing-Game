using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public PlayerInventoryManagment i;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Coin")) return;
        Debug.Log("Cha Ching!");
        Debug.Log("Got a " + other.GetComponent<CoinInfo>().CoinType);
        i.setWorth(other.GetComponent<CoinInfo>().Amount + i.getWorth()) ;
        Debug.Log("You are now worth " + i.getWorth() + " gold coins.") ;
        Destroy(other.gameObject);
    }

}

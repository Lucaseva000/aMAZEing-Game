using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PlayerInventory : MonoBehaviour
{
    public PlayerInventoryManagment inventory;
    public TextMeshProUGUI coinText;

    public void Update()
    {
        setCoinText();
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log(other.tag);
        //If collides with coin
        if (other.CompareTag("Coin"))
        {
            inventory.setWorth(other.GetComponent<CoinInfo>().Amount + inventory.getWorth()) ;
            Destroy(other.gameObject);
        }

        //if collides with item
        if (other.CompareTag("Item"))
        {
            if (inventory.AddItem(other.GetComponent<ItemInstanceHolder>().getItem()))
            {
                Destroy(other.gameObject);
            }
        }
    }
    //Reset the Coin Text to change amount
    public void setCoinText()
    {
        coinText.text = inventory.getWorth().ToString();
    }

}

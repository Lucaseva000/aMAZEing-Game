using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PlayerInventory : MonoBehaviour
{
    public PlayerInventoryManagment i;
    public TextMeshProUGUI numberText;

    public void Update()
    {
        setNumberText();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Coin")) return;
        i.setWorth(other.GetComponent<CoinInfo>().Amount + i.getWorth()) ;
        Destroy(other.gameObject);
        setNumberText();
    }

    public void setNumberText()
    {
        numberText.text = i.getWorth().ToString();
    }

}

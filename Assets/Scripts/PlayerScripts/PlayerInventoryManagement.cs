using UnityEngine;
public class PlayerInventoryManagment : MonoBehaviour{
    public int worth = 0;
    // ---------------------------------------- //
    private void OnTriggerEnter2D(Collider2D other){
        if(!other.CompareTag("Coin")) return;
        Debug.Log("Cha Ching!");
        Debug.Log("Got a " + other.GetComponent<CoinInfo>().CoinType);
        worth += other.GetComponent<CoinInfo>().Amount;
        Debug.Log("You are now worth " + worth + " gold coins.");
        Destroy(other.gameObject);
    }
    // ---------------------------------------- //
}

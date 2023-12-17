using UnityEngine;
[CreateAssetMenu(menuName = "PlayerInventory")]
public class PlayerInventoryManagment : ScriptableObject{
    public int worth;
    // ---------------------------------------- //
   public int getWorth()
    {
        return worth;
    }

    public void setWorth(int i)
    {
        worth = i;
    }
    // ---------------------------------------- //
}

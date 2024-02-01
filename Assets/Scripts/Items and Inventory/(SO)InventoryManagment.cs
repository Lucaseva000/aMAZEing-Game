using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "PlayerInventory")]
public class PlayerInventoryManagment : ScriptableObject{
    //Coin worth
    public int worth;
    //Inventory Items
    public int maxItems = 3;
    public List<ItemInstance> items = new();


   public int getWorth()
    {
        return worth;
    }

    public void setWorth(int i)
    {
        worth = i;
    }
    public bool AddItem(ItemInstance itemToAdd)
    {
        //Will search for an empty space in the inventory
        for(int i = 0; i< items.Count; i++)
        {
            if(items[i] == null)    
            {
                items[i] = itemToAdd;
                return true;
            }
        }
        //Will Add the item to the end of the list
        if(items.Count< maxItems)
        {
            items.Add(itemToAdd);
            return true;
        }
        return false;
    }
}

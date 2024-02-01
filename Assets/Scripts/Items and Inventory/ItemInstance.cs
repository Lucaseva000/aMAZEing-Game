using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemInstance
{
    public ItemData itemType;
    public ItemInstance(ItemData z)
    {
        itemType = z;
    }
    //all sorts of stuff can be added here and in the itemData scripts. The itemdata scripts will be the ones that are used over and over though
}

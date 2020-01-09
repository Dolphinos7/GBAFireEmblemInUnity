using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
This class handles the functionality of the Inventory system. The functionality is implemented using a List<Items>
*/
public class Inventory
{
    private List<Item> itemList;
    private static readonly capacity = 6;
    
    public Inventory(){
        itemList = new List<Item>();
    }

    public boolean GiveItem(Item toAdd){
        if(itemList.Count<=6){
            itemList.Add(toAdd);
            return true;
        }
        return false; 
    }

    public boolean ThrowOutItem(Item toRemove){
        return itemList.Remove(toRemove)
    }
    
    public boolean SwapItem(Item toAdd, Item toRemove){
        if(itemList.Contains(toRemove)){
            ThrowOutItem(toRemove);
            return GiveItem(toAdd);
        }
        return false;
    }

}

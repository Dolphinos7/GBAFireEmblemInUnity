using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : List<Item>
{
    private List<Item> itemList;
    private readonly int capacity = 6;
    
    public Inventory()
    {
        itemList = new List<Item>();
    }

    public bool GiveItem(Item toAdd)
    {
        if (itemList.Count <= 6)
        {
            itemList.Add(toAdd);
            return true;
        }
        return false;
    }

    public bool ThrowOutItem(Item toRemove)
    {
        return itemList.Remove(toRemove);
    }

    public bool SwapItem(Item toAdd, Item toRemove)
    {
        if (itemList.Contains(toRemove))
        {
            ThrowOutItem(toRemove);
            return GiveItem(toAdd);
        }
        return false;
    }

    /*
    public Inventory(){

    }

    public void showContents(){
        foreach (Item item in this){
            Debug.Log(item.ToString());
        }
    }

    public void addIronSword(){
        Add(new Weapon("iron sword"));
    }
    */
}

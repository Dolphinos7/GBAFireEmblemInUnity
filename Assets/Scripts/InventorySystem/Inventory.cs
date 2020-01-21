using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> itemList;
    private readonly int capacity = 6;

    public Inventory()
    {
        itemList = new List<Item>();
    }

    public bool GiveItem(Item toAdd)
    {
        if (itemList.Count <= capacity)
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

    public Item Get(int index)
    {
        Item toReturn = null;
        toReturn = itemList[index];
        return toReturn;
    }

    public bool add(Item item)
    {
        if (itemList.Count < capacity)
        {
            itemList.Add(item);
            return true;
        }
        else
        {
            return false;
        }
    }

    public override string ToString(){
        string toReturn = "";
        foreach (Item i in itemList){
            toReturn += i.ToString();
        }
        return toReturn;
    }

    public int Count(){
        return itemList.Count;
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

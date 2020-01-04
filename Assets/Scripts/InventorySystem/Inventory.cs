using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : List<Item>
{
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
}

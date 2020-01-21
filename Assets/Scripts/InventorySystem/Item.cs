using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string name;
    public bool isWeapon = false;
    public bool isUsableItem = false;
    public Item(string name){
        this.name = name;
    }

    public override string ToString(){
        return name;
    }
}

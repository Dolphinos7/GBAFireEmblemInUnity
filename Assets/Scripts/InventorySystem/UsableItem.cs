using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableItem : Item
{
    public string description = "";
    public UsableItem(string name) : base(name)
    {
        isUsableItem = true;
        this.name = "TestUsableItem";
        description = "This is the description box and this is the text that will go in it here it is just as a test";
    }





}
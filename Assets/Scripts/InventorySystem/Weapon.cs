using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{

    public int might;
    public int crit;
    public int hit;
    public int range;
    public char requiredRank;
    public int weight;


    public Weapon(string name) : base(name)
    {
        isWeapon = true;
        if (name.Equals("iron sword"))
        {

            requiredRank = 'E';
            crit = 0;
            weight = 0;
            range = 1;
            might = 5;
            hit = 90;
        }
        else if (name.Equals("steel sword"))
        {

            requiredRank = 'D';
            crit = 0;
            weight = 3;
            range = 1;
            might = 8;
            hit = 75;
        }
    }

    public override string ToString()
    {
        string toReturn = "[";
        toReturn += "might " + might + ", crit " + crit;
        return toReturn;
    }

}

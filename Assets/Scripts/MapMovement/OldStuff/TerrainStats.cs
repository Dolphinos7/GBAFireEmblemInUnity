using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainStats
{
    
    private int movementCost;
    private int defenseBonus;
    private int avoidBonus;




    public TerrainStats(int moveCost, int defBonus, int avoBonus)
    {
        movementCost = moveCost;
        defenseBonus = defBonus;
        avoidBonus = avoBonus;

    }



}

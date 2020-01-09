using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Inventory inventory;
    public string BASE_STATS;
    public bool waiting = false;
    private CharacterStats stats;

    public void init()
    {
        generateStats(BASE_STATS);
        inventory = new Inventory();
        //BELOW IS FOR TESTING
        // OLD inventory.addIronSword();



    }

    public CharacterStats getStats()
    {
        return stats;
    }

    public void generateStats(string baseStats)
    {
        if (baseStats.Equals("hector"))
        {
            stats = new CharacterStats(1, 19, 7, 4, 5, 3, 8, 0, 13, 5, .9, .6, .45, .35, .3, .5, .25);
        }
    }

}

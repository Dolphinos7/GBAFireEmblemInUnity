using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HectorInfo : MonoBehaviour
{
    private CharacterStats hectorStats = new CharacterStats(1, 19, 7, 4, 5, 3, 8, 0, 13, 5, .9, .6, .45, .35, .3, .5, .25);
    private GameObject hector;
    public void Start()
    {
        hector = gameObject;
        
    }

    public GameObject getHector()
    {
        return hector;
    }

    public CharacterStats getHectorStats()
    {
        return hectorStats;
    }




}

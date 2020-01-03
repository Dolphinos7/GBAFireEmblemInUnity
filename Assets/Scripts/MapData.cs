using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{

    //Declaring fields
    private ArrayList enemies;
    private ArrayList players;
    private ArrayList allies;
    Grid grid;
    void Start()
    {

        grid = new Grid(50, 50);

        //Initializes ArrayLists

        enemies = new ArrayList();
        players = new ArrayList();
        allies = new ArrayList();




        //Creates an arraylist of all starting enemies
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Enemy").GetLength(0); i++)
        {
            enemies.Add(GameObject.FindGameObjectsWithTag("Enemy")[i]);

        }

        //Creates an arraylist of all starting player controlled characters
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Player").GetLength(0); i++)
        {
            players.Add(GameObject.FindGameObjectsWithTag("Player")[i]);

        }

        //Creates an arraylist of all starting allies
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Ally").GetLength(0); i++)
        {
            players.Add(GameObject.FindGameObjectsWithTag("Ally")[i]);

        }
    }


    public ArrayList getEnemies()
    {
        return enemies;
    }
    public ArrayList getPlayers()
    {
        return players;
    }
    public ArrayList getAllies()
    {
        return allies;
    }
    public Grid getGrid()
    {
        return grid;
    }



}

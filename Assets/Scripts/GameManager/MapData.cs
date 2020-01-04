using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{

    //Declaring fields
    private List<GameObject> enemies;
    private List<GameObject> players;
    private List<GameObject> allies;
    public List<GameObject> tiles;
    Grid grid;
    void Start()
    {

        grid = new Grid(50, 50);

        //Initializes ArrayLists

        enemies = new List<GameObject>();
        players = new List<GameObject>();
        allies = new List<GameObject>();
        tiles = new List<GameObject>();
        




        //Creates an arraylist of all starting enemies
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(enemy);

        }

        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Terrain").GetLength(0); i++)
        {
            tiles.Add(GameObject.FindGameObjectsWithTag("Terrain")[i]);

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


    public List<GameObject> getEnemies()
    {
        return enemies;
    }
    public List<GameObject> getPlayers()
    {
        return players;
    }
    public List<GameObject> getAllies()
    {
        return allies;
    }
    public Grid getGrid()
    {
        return grid;
    }



}

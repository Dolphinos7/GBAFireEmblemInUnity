using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    int width;
    int height;
    private GameObject[,] gridArray;

    public Grid(int width, int height)
    {
        this.width = width;
        this.height = height;
        gridArray = new GameObject[width, height];

        for (int i = 0; i < gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < gridArray.GetLength(1); j++)
            {
                var terrainArray = GameObject.FindGameObjectsWithTag("Terrain");
                GameObject toAdd = null;
                for (int k = 0; k < terrainArray.GetLength(0); k++)
                {
                    if (terrainArray[k].GetComponent<Transform>().position.x + .5 == i && terrainArray[k].GetComponent<Transform>().position.y + .5 == j)
                    {
                        toAdd = terrainArray[k];
                        k = terrainArray.GetLength(0);
                    }
                    gridArray[i, j] = toAdd;
                }
            }

        }

    }

    public GameObject[,] getGridArray(){
        return gridArray;
    }
    
}

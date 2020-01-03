﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //public bool isParent = false; Failure to find edge tiles
    public bool flyable = true;
    public bool attackable = false;
    public bool attackableHighlight = false;
    public bool walkable = true;
    public bool current = false;
    public bool target = false;
    public bool selectable = false;
    public bool isEdge = false;
    public bool staffable = false;
    public bool staffableHighlight = false;

    public List<Tile> adjacencyList = new List<Tile>();

    //Needed BFS (breadth first search)
    public bool visited = false;
    public Tile parent = null;
    public int distance = 0;
    private int moveCost = 1;
    public string tileName;

    // Update is called once per frame
    void Start()
    {
        if (tileName == "forest")
        {
            moveCost = 2;
        }
        if (tileName == "cliff")
        {
            walkable = false;
        }
        if (tileName == "mountain")
        {
            moveCost = 3;
        }
        if (tileName == "peak")
        {
            moveCost = 4;
        }
    }
    void Update()
    {
        
        
        if (current)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("MovementTile");
        }
        /*
        else if (isEdge){
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Selector");
        }
        */
        else if (target)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Selector");
        }
        else if (attackableHighlight){
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("AttackTile");
        }
        else if (staffableHighlight){
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("StaffTile");
        }
        else if (selectable)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("MovementTile");
        }
        /*
        Failure to find edge tiles
        else if(!isParent && selectable){
            Debug.Log("working");
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Selector");
        }
        */
        else
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Empty");
        }
        
    }

    public void Reset()
    {
        adjacencyList.Clear();

        current = false;
        target = false;
        selectable = false;
        attackable = false;
        attackableHighlight = false;
        staffable = false;
        staffableHighlight = false;

        visited = false;
        parent = null;
        distance = 0;

    }

    public void FindNeighbors()
    {
        Reset();

        CheckTile(Vector2.up);
        CheckTile(-Vector2.up);
        CheckTile(Vector2.right);
        CheckTile(-Vector2.right);
    }

    public void CheckTile(Vector2 direction)
    {
        Collider2D collider = Physics2D.OverlapBox(new Vector2(transform.position.x + direction.x, transform.position.y + direction.y), new Vector2(.25f, .25f), 0f);
        if (collider != null)
        {
            Tile tile = collider.GetComponent<Tile>();
            if (tile != null && (tile.walkable || flyable))
            {
                adjacencyList.Add(tile);
            }
        }
    }

    public int getMoveCost()
    {
        return moveCost;
    }

    public void OnMouseEnter()
    {
        GameObject.Find("GameMaster").GetComponent<UIController>().getCursor().transform.position = transform.position;
    }
}
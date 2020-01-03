using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldTacticsMove : MonoBehaviour
{
    public bool canWalk = true;
    public bool canFly = false;
    public int move = 5;//Set to player movement
    public int[] attackRange;
    public bool moving = false;
    private Vector3 heading;
    private Vector3 velocity;
    public float movespeed = 2;
    public List<Tile> edgeTiles = new List<Tile>();
    List<Tile> selectableTiles = new List<Tile>();
    List<Tile> attackableTiles = new List<Tile>();
    GameObject[] tiles;
    Stack<Tile> path = new Stack<Tile>();
    Tile currentTile;

    protected void init()
    {
        tiles = GameObject.FindGameObjectsWithTag("Terrain");
        attackRange = new int[2];
        findAttackRange();

    }

    public void getCurrentTile()
    {
        currentTile = getTargetTile(gameObject);
        currentTile.current = true;
    }

    public void findAttackRange()
    {
        attackRange[0] = 1;
        attackRange[1] = 2;

    }

    public Tile getTargetTile(GameObject target)
    {
        Tile toReturn = null;
        Vector3 halfExtents = new Vector3(.25f, .25f, 1f);
        Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(target.transform.position.x, target.transform.position.y), new Vector2(.25f, .25f), 0f);

        foreach (Collider2D collider in colliders)
        {

            if (collider.tag == "Terrain")
            {
                Tile tile = collider.GetComponent<Tile>();
                toReturn = tile;
            }
        }
        return toReturn;
    }

    public void computeAdjacencyList()
    {
        foreach (GameObject tile in tiles)
        {
            Tile t = tile.GetComponent<Tile>();
            t.FindNeighbors();
        }
    }

    public void findSelectableTiles()
    {
        computeAdjacencyList();
        getCurrentTile();

        TileList process = new TileList();
        currentTile.distance = 0;
        process.Add(currentTile);
        currentTile.visited = true;

        //currentTile.parent = null


        while (process.Count > 0)
        {
            process.Sort(process.getComparator());
            Tile t = process[0];
            process.RemoveAt(0);
            selectableTiles.Add(t);
            t.selectable = true;
            if (canWalk && !canFly)
            {
                if (t.walkable)
                {
                    bool edge = true;

                    foreach (Tile tile in t.adjacencyList)
                    {
                        if (tile.walkable && t.distance + tile.getMoveCost() <= move)
                        {
                            edge = false;
                        }
                        if (!tile.visited && tile.walkable && t.distance + tile.getMoveCost() <= move)
                        {
                            tile.parent = t;
                            tile.visited = true;
                            tile.distance = t.distance + tile.getMoveCost();
                            process.Add(tile);

                            /*else if (t.distance <= move + attackRange){
                                tile.visited = true;
                                attackableTiles.Add(t);
                                t.attackable = true;

                            }
                            */

                        }
                        /*
                        else if (t.getMoveCost() > 1 && tile.walkable && t.distance + tile.getMoveCost() <= move)
                        {

                            if (!selectableTiles.Contains(tile))
                            {
                                Debug.Log("here");
                                tile.parent = t;
                                tile.visited = true;
                                foreach (Tile moreTile in tile.adjacencyList)
                                {
                                    moreTile.visited = false;
                                }
                                tile.distance = 1 + t.distance + tile.getMoveCost() - 1;
                                process.Add(tile);
                            }
                        }
                        */

                    }
                    if (edge)
                    {
                        //edgeTiles.Add(t);
                    }
                }

            }

        }
    }

    public void checkPaths(List<Tile> tiles)
    {
        foreach (Tile tile in tiles)
        {
            Tile next = tile;
            int cost = 0;
            while (next != null)
            {
                if (next.parent != null)
                {
                    cost += next.getMoveCost();
                }
                next = next.parent;
            }
            if (cost > move)
            {
                tile.selectable = false;
            }
            else
            {
                tile.selectable = true;
            }
        }
    }

    public void checkPaths()
    {
        List<Tile> tileList = new List<Tile>();
        foreach (GameObject obj in tiles)
        {
            tileList.Add(obj.GetComponent<Tile>());
        }
        checkPaths(tileList);
    }
    public void moveToTile(Tile tile)
    {
        path.Clear();
        tile.target = true;
        moving = true;

        Tile next = tile;
        while (next != null)
        {
            path.Push(next);
            next = next.parent;
        }
    }

    public void moveCharacter()
    {
        if (path.Count > 0)
        {
            Tile t = path.Peek();
            Vector3 target = t.transform.position;

            if (Vector3.Distance(transform.position, target) >= .05f)
            {
                calculateHeading(target);
                setHorizontalVelocity();

                transform.position += velocity * Time.deltaTime;
            }
            else
            {
                transform.position = target;
                path.Pop();
            }
        }
        else
        {
            removeSelectableTiles();
            removeAttackableTiles();
            moving = false;
        }
    }

    protected void removeSelectableTiles()
    {
        if (currentTile != null)
        {
            currentTile.current = false;
            currentTile = null;
        }
        foreach (Tile tile in selectableTiles)
        {
            tile.Reset();
        }

        selectableTiles.Clear();

    }

    public void removeAttackableTiles()
    {
        if (currentTile != null)
        {
            currentTile.current = false;
            currentTile = null;
        }
        foreach (Tile tile in attackableTiles)
        {
            tile.Reset();
        }
        attackableTiles.Clear();
    }

    public void calculateHeading(Vector3 target)
    {
        heading = target - transform.position;
        heading.Normalize();

    }

    public void setHorizontalVelocity()
    {
        velocity = heading * movespeed;
    }

    /*
        public int pathCost(Tile tile){
            Tile[] tiles = path.ToArray();
            int toReturn = 0;
            foreach (Tile t in tiles){
                toReturn += t.moveCost;
            }
            return toReturn;
        }
        */

    public void highlightAttackableTiles()
    {
        foreach (Tile t in selectableTiles)
        {
            foreach (Tile tile in t.adjacencyList)
            {
                if (!selectableTiles.Contains(tile) && attackRange[0] == 1 && attackRange[0] == 1)
                {
                    attackableTiles.Add(tile);
                    tile.attackable = true;
                }
            }

        }
    }

    public List<Tile> getTilesInRadius(int radius)
    {
        List<Tile> toReturn = new List<Tile>();
        int rowWidth = 1;
        int curHeight = radius;
        for (int i = 0; i < radius * 2 + 1; i++)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y + curHeight), new Vector2(rowWidth - .25f, .25f), 0);

            foreach(Collider2D collider in colliders){
                if (collider.tag == "Terrain"){
                    toReturn.Add(collider.GetComponent<Tile>());
                }
            }

            if (i < radius)
            {
                rowWidth += 2;
            }
            else
            {
                rowWidth -= 2;
            }
            curHeight -= 1;

        }


        return toReturn;
    }

}

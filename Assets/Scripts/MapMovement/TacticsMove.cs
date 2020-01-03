using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsMove : MonoBehaviour
{
    public bool canWalk = true;
    public bool canFly = false;
    public int move;
    public int[] staffRange;
    public int[] attackRange;
    public bool moving = false;
    private Vector3 heading;
    private Vector3 velocity;
    public float movespeed = 2;
    public List<Tile> edgeTiles = new List<Tile>();
    List<Tile> selectableTiles = new List<Tile>();
    List<Tile> attackableTiles = new List<Tile>();
    List<Tile> staffableTiles = new List<Tile>();
    GameObject[] tiles;
    Stack<Tile> path = new Stack<Tile>();
    Tile currentTile;


    //Stuff that needs definite updates later

    protected void init()
    {
        tiles = GameObject.FindGameObjectsWithTag("Terrain");

        findAttackRange();
        findMovementRange();
        findStaffRange();
    }

    public void findStaffRange()
    {
        staffRange = new int[2];
        staffRange[0] = 1;
        staffRange[1] = 3;
        if (staffRange[0] > staffRange[1])
        {
            Debug.Log("Staff range is set incorrectly, staffRange[0] must be lower bound");
        }
    }


    public void findMovementRange()
    {
        move = GetComponent<PlayerCharacter>().getStats().getMovement();
    }

    public void findAttackRange()
    {
        attackRange = new int[2];
        attackRange[0] = 1;
        attackRange[1] = 2;
        if (attackRange[0] > attackRange[1])
        {
            Debug.Log("Attack range is set incorrectly, attackRange[0] must be lower bound");
        }
    }

    public void findSelectableTiles() //Needs updates for flying (Peg / Draco Knights) and climbing units (Brigands)
    {
        computeAdjacencyList();
        getCurrentTile();

        TileList process = new TileList();
        currentTile.distance = 0;
        process.Add(currentTile);
        currentTile.visited = true;
        //Source tile's parent is null

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

                    foreach (Tile tile in t.adjacencyList)
                    {

                        if (!tile.visited && tile.walkable && t.distance + tile.getMoveCost() <= move)
                        {

                            tile.parent = t;
                            tile.visited = true;
                            tile.distance = t.distance + tile.getMoveCost();
                            process.Add(tile);
                        }
                    }

                }

            }

        }

    }






    //Stuff I dont anticipate touching


    public void getCurrentTile()
    {
        currentTile = getTargetTile(gameObject);
        currentTile.current = true;
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



    public void findEdgeTiles()
    {
        foreach (Tile t in selectableTiles)
        {
            bool isEdge = false;
            foreach (Tile tile in t.adjacencyList)
            {
                if (!selectableTiles.Contains(tile))
                {
                    isEdge = true;
                }
            }
            if (isEdge)
            {
                t.isEdge = true;
                edgeTiles.Add(t);
            }
        }
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
            removeStaffableTiles();
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

    public void removeStaffableTiles()
    {
        if (currentTile != null)
        {
            currentTile.current = false;
            currentTile = null;
        }
        foreach (Tile tile in staffableTiles)
        {
            tile.Reset();
        }
        staffableTiles.Clear();
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

    public void highlightAttackableTiles()
    {
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            tiles[i].GetComponent<Tile>().visited = false;
            tiles[i].GetComponent<Tile>().distance = -1;
        }
        foreach (Tile t in selectableTiles)
        {
            foreach (GameObject resetTile in tiles)
            {
                resetTile.GetComponent<Tile>().distance = -1;
                resetTile.GetComponent<Tile>().visited = false;
            }
            Queue<Tile> process = new Queue<Tile>();
            t.distance = 0;
            process.Enqueue(t);
            currentTile.visited = true;

            while (process.Count > 0)
            {
                Tile currentTile = process.Dequeue();
                if (currentTile.distance >= attackRange[0] && currentTile.distance <= attackRange[1])
                {
                    currentTile.attackable = true;

                    if (!selectableTiles.Contains(currentTile) && !attackableTiles.Contains(currentTile))
                    {
                        currentTile.attackableHighlight = true;
                    }
                    if (currentTile.attackable)
                    {
                        attackableTiles.Add(currentTile);
                    }
                }
                foreach (Tile tile in currentTile.adjacencyList)
                {
                    if (!tile.visited && currentTile.distance + 1 <= attackRange[1])
                    {
                        tile.visited = true;
                        tile.distance = currentTile.distance + 1;
                        process.Enqueue(tile);
                    }
                }
            }
        }
    }


    public void highlightStaffableTiles()
    {
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            tiles[i].GetComponent<Tile>().visited = false;
            tiles[i].GetComponent<Tile>().distance = -1;
        }
        foreach (Tile t in selectableTiles)
        {
            foreach (GameObject resetTile in tiles)
            {
                resetTile.GetComponent<Tile>().distance = -1;
                resetTile.GetComponent<Tile>().visited = false;
            }
            Queue<Tile> process = new Queue<Tile>();
            t.distance = 0;
            process.Enqueue(t);
            currentTile.visited = true;

            while (process.Count > 0)
            {
                Tile currentTile = process.Dequeue();
                if (currentTile.distance >= staffRange[0] && currentTile.distance <= staffRange[1])
                {
                    currentTile.staffable = true;

                    if (!selectableTiles.Contains(currentTile) && !attackableTiles.Contains(currentTile) && !staffableTiles.Contains(currentTile))
                    {
                        currentTile.staffableHighlight = true;
                    }
                    if (currentTile.staffable)
                    {
                        staffableTiles.Add(currentTile);
                    }
                }
                foreach (Tile tile in currentTile.adjacencyList)
                {
                    if (!tile.visited && currentTile.distance + 1 <= staffRange[1])
                    {

                        tile.visited = true;
                        tile.distance = currentTile.distance + 1;
                        process.Enqueue(tile);
                    }
                }
            }
        }
    }


}

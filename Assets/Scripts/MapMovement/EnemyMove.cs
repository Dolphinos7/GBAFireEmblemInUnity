using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : TacticsMove
{
    private bool tilesSelected = false;
    bool flip = true;
    GameObject target = null;
    Tile tileToMoveTo = null;
    float distanceToTarget = Mathf.Infinity;
    Tile closestMoveableTile = null;
    public bool hasEndedMove = false;
    void Start()
    {
        init();
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("RedHector");
    }
    void Update()
    {


        if (!TurnManager.isPlayerPhase)
        {
            if (flip)
            {
                flip = false;
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("RedHector");
                findSelectableTiles();
                highlightAttackableTiles();
                highlightStaffableTiles();
                findEdgeTiles();
                FindNearestTarget();
                
                FindTileToMoveTo();
                //Debug.Log(tileToMoveTo);
                
                if (tileToMoveTo != null)
                {
                    
                    BuildPathToTargetTile();
                    moveToTile(tileToMoveTo);
                    //moveMode = false;
                }
                foreach (GameObject t in tiles)
                {
                    t.GetComponent<Tile>().HideHighlights();
                }
                

            }
            else if (moving && tileToMoveTo != null)
            {
                moveCharacter();
            }
            else if (!hasEndedMove)
            {
                endMove();
                hasEndedMove = true;
            }
        }
        

        /*

                    if (!moving && moveMode)
                    {
                        if (!tilesSelected)
                        {
                            findSelectableTiles();
                            highlightAttackableTiles();
                            highlightStaffableTiles();
                            FindNearestTarget();
                            FindClosestMoveableTile();

                            tilesSelected = true;
                        }
                    }
                    else
                    {
                        moveCharacter();
                        tilesSelected = false;
                    }
                }
                */
    }

    public void endMove()
    {
        tileToMoveTo = null;
        distanceToTarget = Mathf.Infinity;
        tilesSelected = false;
        target = null;
        closestMoveableTile = null;
        
        GetComponent<Character>().waiting = true;
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("GrayscaleHector");
        removeSelectableTiles();
        removeAttackableTiles();
        removeStaffableTiles();
        removeEdgeTiles();
        moving = false;
    }


    public void BuildPathToTargetTile()
    {
        foreach (GameObject resetTile in tiles)
        {
            resetTile.GetComponent<Tile>().distance = -1;
            resetTile.GetComponent<Tile>().visited = false;
        }
        TileList process = new TileList();
        currentTile.distance = 0;
        process.Add(currentTile);
        currentTile.visited = true;
        currentTile.parent = null;

        while (process.Count > 0)
        {
            process.Sort(process.getComparator());
            Tile t = process[0];
            process.RemoveAt(0);
            bool endLoop = false;
            if (t.isWalkable && !endLoop)
            {
                
                foreach (Tile tile in t.adjacencyList)
                {
                    
                    if (!tile.visited && tile.walkable && t.distance + tile.getMoveCost() <= move && !endLoop)
                    {
                        
                        tile.parent = t;
                        tile.visited = true;
                        tile.distance = t.distance + tile.getMoveCost();
                        process.Add(tile);
                    }
                    if (tile.Equals(tileToMoveTo))
                    {
                        endLoop = true;
                    }

                }

            }



        }



    }

    public void FindTileToMoveTo()
    {
        Tile tileToMoveTo = null;
        float nearestDistance = Mathf.Infinity;
        if (!attackableTiles.Contains(target.GetComponent<PlayerMove>().getTargetTile(target)))
        {
            
            foreach (Tile t in edgeTiles)
            {
                
                foreach (GameObject resetTile in tiles)
                {
                    resetTile.GetComponent<Tile>().distance = -1;
                    resetTile.GetComponent<Tile>().visited = false;
                }
                TileList process = new TileList();
                t.distance = 0;
                process.Add(t);
                t.visited = true;

                while (process.Count > 0)
                {
                    
                    bool finishLoop = false;
                    process.Sort(process.getComparator());
                    Tile currentTile = process[0];
                    process.RemoveAt(0);
                    //Debug.Log(currentTile.transform.position);
                    //if (currentTile.transform.position == new Vector3(1.5f, 7.5f, 0)){
                    //    Debug.DrawLine(currentTile.transform.position, currentTile.transform.position + Vector3.up, Color.white, 10f);
                    //    foreach (Tile temp in currentTile.adjacencyList){
                    //    }
                    //}
                    if (currentTile.Equals(target.GetComponent<PlayerMove>().currentTile))
                    {  
                        finishLoop = true;
                        process.Clear();
                        if (currentTile.distance - currentTile.getMoveCost() + 1 < nearestDistance)
                        {

                            tileToMoveTo = t;

                            nearestDistance = currentTile.distance - currentTile.getMoveCost() + 1;
                        }
                    }

                    foreach (Tile tile in currentTile.adjacencyList)
                    {
                        if (!tile.visited && tile.isWalkable && finishLoop == false)
                        {
                            tile.parent = currentTile;
                            tile.visited = true;
                            tile.distance = currentTile.distance + tile.getMoveCost();
                            process.Add(tile);
                        }
                    }
                }

            }
            this.tileToMoveTo = tileToMoveTo;
        }


        else
        {
            this.tileToMoveTo = null;
        }
    }

    public void FindNearestTarget()
    {


        List<GameObject> targets = GameObject.Find("GameMaster").GetComponent<MapData>().getPlayers();
        GameObject nearest = null;
        float nearestDistance = Mathf.Infinity;
        foreach (GameObject obj in targets)
        {
            //computeAdjacencyList();
            foreach (GameObject resetTile in tiles)
            {
                resetTile.GetComponent<Tile>().distance = -1;
                resetTile.GetComponent<Tile>().visited = false;
            }
            obj.GetComponent<PlayerMove>().currentTile = getTargetTile(obj);
            Tile t = obj.GetComponent<PlayerMove>().currentTile;
            TileList process = new TileList();
            t.distance = 0;
            process.Add(t);
            t.visited = true;

            while (process.Count > 0)
            {
                bool finishLoop = false;
                process.Sort(process.getComparator());
                Tile currentTile = process[0];
                process.RemoveAt(0);
                this.getTargetTile(gameObject);
                if (currentTile.Equals(this.currentTile))
                {
                    finishLoop = true;
                    process.Clear();
                    if (currentTile.distance - currentTile.getMoveCost() + 1 < nearestDistance)
                    {
                        nearest = obj;

                        nearestDistance = currentTile.distance - currentTile.getMoveCost() + 1;
                        distanceToTarget = nearestDistance;
                    }
                }

                foreach (Tile tile in currentTile.adjacencyList)
                {
                    if (!tile.visited && tile.isWalkable && finishLoop == false)
                    {

                        tile.parent = currentTile;
                        tile.visited = true;
                        tile.distance = currentTile.distance + tile.getMoveCost();
                        process.Add(tile);
                    }
                }
            }
        }
        target = nearest;
    }

        public void refresh()
    {
        GetComponent<Character>().waiting = false;
        hasEndedMove = false;
        flip = true;
    }
}

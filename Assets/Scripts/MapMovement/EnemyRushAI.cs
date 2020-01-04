using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRushAI : TacticsMove
{
    public bool canAttack;
    bool flip = true;
    GameObject target = null;
    Tile tileToMoveTo = null;
    float distanceToTarget = Mathf.Infinity;
    public bool hasEndedMove = false;
    void Start()
    {
        init();
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("RedHector");
    }
    void Update()
    {


        if (!TurnManager.isPlayerPhase && TurnManager.currentEnemy.Equals(gameObject))
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

                FindActionToTake();
                //Debug.Log(tileToMoveTo);

                if (tileToMoveTo != null)
                {

                    BuildPathToTargetTile();
                    moveToTile(tileToMoveTo);
                    //moveMode = false;
                }
                else if (canAttack)
                {
                    FindTileToAttackFrom();
                    BuildPathToTargetTile();
                    moveToTile(tileToMoveTo);
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
            else if (canAttack)
            {
                //Execute Attack
                Debug.Log("Attacking");
                canAttack = false;
            }
            else if (!hasEndedMove)
            {
                endMove();
                hasEndedMove = true;
            }
        }
    }
    public void endMove()
    {
        if (TurnManager.enemyQueue.Count != 0)
        {
            TurnManager.GetNextEnemy();
        }
        tileToMoveTo = null;
        distanceToTarget = Mathf.Infinity;
        target = null;

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
                    string tagOn = "";
                    foreach (Collider2D collider in tile.GetOnTopOf())
                    {
                        if (collider.gameObject.tag != null)
                        {
                            if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Enemy")
                            {
                                tagOn = collider.gameObject.tag;
                            }
                            //Debug.Log(tagOn);
                        }
                    }

                    if (!tile.visited && (tile.walkable || tagOn == gameObject.tag) && t.distance + tile.getMoveCost() <= move && !endLoop)
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

    public void FindTileToAttackFrom()
    {
        //Get Tile Target Is On
        //Find tile to move to candidates which are in selectable tiles and the distance from those tiles to taret is within attack range bounds
        //For now move to highest cost tile or last tile scanned
        int tileCost = 0;
        Tile bestTile = null;
        Tile targetTile = target.GetComponent<PlayerMove>().currentTile;
        foreach (GameObject resetTile in tiles)
        {
            resetTile.GetComponent<Tile>().distance = -1;
            resetTile.GetComponent<Tile>().visited = false;
        }
        Queue<Tile> process = new Queue<Tile>();
        targetTile.distance = 0;
        targetTile.visited = false;
        process.Enqueue(targetTile);
        while (process.Count > 0)
        {
            Tile t = process.Dequeue();
            //Add more attack logic here
            //Debug.Log(t.distance);
            if (t.distance >= attackRange[0] && t.distance <= attackRange[1] && selectableTiles.Contains(t) && t.getMoveCost() >= tileCost)
            {
                tileCost = t.getMoveCost();
                bestTile = t;
            }
            foreach (Tile tile in t.adjacencyList)
            {
                if (!tile.visited && t.distance + 1 <= attackRange[1])
                {
                    tile.visited = true;
                    tile.distance = t.distance + 1;
                    process.Enqueue(tile);
                }
            }
        }
        tileToMoveTo = bestTile;

    }

    public void FindActionToTake()
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
            canAttack = true;
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

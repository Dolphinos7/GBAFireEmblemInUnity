using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static bool isPlayerPhase = true;
    public static Queue<GameObject> enemyQueue;
    public static GameObject currentEnemy;
    public static GameObject playerSelected = null;
    GameObject cursor;

    void Start(){
        cursor = GameObject.Find("Cursor");
    }
    public static void makePlayerPhase()
    {
        GameObject.Find("GameMaster").GetComponent<UIController>().enableCursor();
        isPlayerPhase = true;
        FindObjectOfType<TurnManager>().clearTileLists();
        foreach (GameObject t in FindObjectOfType<MapData>().tiles)
        {
            t.GetComponent<Tile>().Reset();
        }

        foreach (GameObject player in FindObjectOfType<MapData>().getPlayers())
        {
            player.GetComponent<PlayerCharacter>().refresh();
        }

        foreach (GameObject character in FindObjectOfType<MapData>().getEnemies())
        {
            character.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("RedHector");
        }
    }

    public static void makeEnemyPhase()
    {
                GameObject.Find("GameMaster").GetComponent<UIController>().disableCursor();
        GenerateEnemyQueue();
        FindObjectOfType<TurnManager>().clearTileLists();
        foreach (GameObject t in FindObjectOfType<MapData>().tiles)
        {
            t.GetComponent<Tile>().Reset();
        }
        foreach (GameObject character in FindObjectOfType<MapData>().getEnemies())
        {
            character.GetComponent<EnemyRushAI>().refresh();
        }
        isPlayerPhase = false;
        GenerateEnemyQueue();
        if (enemyQueue.Count != 0)
        {
            GetNextEnemy();
        }
        //Refresh enemies
    }

    public void findSelectedPlayer(){
        if (Input.GetMouseButtonUp(0)){
            bool setSelected = false;
            Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(cursor.transform.position.x, cursor.transform.position.y), new Vector2(.25f, .25f), 0f);
            foreach (Collider2D collider in colliders){
                if (collider.tag == "Player" &&  !collider.gameObject.GetComponent<PlayerCharacter>().waiting){
                    setSelected = true;
                    GameObject.Find("GameMaster").GetComponent<UIController>().enableCursor();
                    playerSelected = collider.gameObject;
                    playerSelected.GetComponent<PlayerCharacter>().selected = true;
                    playerSelected.GetComponent<PlayerMove>().tilesSelected = false;
                }
            }
            if (!setSelected){
                playerSelected = null;
            }
        }
    }
    public void Update()
    {
        if (isPlayerPhase)
        {
            if (cursor.activeSelf){
            findSelectedPlayer();
            }
            bool changeTurn = true;
            foreach (GameObject character in GetComponent<MapData>().getPlayers())
            {
                if (!character.GetComponent<PlayerCharacter>().waiting)
                {
                    changeTurn = false;
                }
            }
            if (changeTurn == true)
            {
                makeEnemyPhase();
            }
        }
        else
        {
            bool changeTurn = true;
            foreach (GameObject character in GetComponent<MapData>().getEnemies())
            {
                if (!character.GetComponent<Character>().waiting)
                {
                    changeTurn = false;
                }
            }
            if (changeTurn == true)
            {
                makePlayerPhase();
            }


        }
    }

    public void clearTileLists()
    {
        foreach (GameObject character in FindObjectOfType<MapData>().getPlayers())
        {
            character.GetComponent<PlayerMove>().removeEdgeTiles();
            character.GetComponent<PlayerMove>().removeStaffableTiles();
            character.GetComponent<PlayerMove>().removeAttackableTiles();
            character.GetComponent<PlayerMove>().removeSelectableTiles();
        }


        foreach (GameObject character in FindObjectOfType<MapData>().getEnemies())
        {
            character.GetComponent<EnemyRushAI>().removeEdgeTiles();
            character.GetComponent<EnemyRushAI>().removeStaffableTiles();
            character.GetComponent<EnemyRushAI>().removeAttackableTiles();
            character.GetComponent<EnemyRushAI>().removeSelectableTiles();
        }
    }

    public static void GenerateEnemyQueue()
    {
        enemyQueue = new Queue<GameObject>();
        foreach (GameObject obj in FindObjectOfType<MapData>().getEnemies())
        {
            enemyQueue.Enqueue(obj);
        }
    }

    public static void GetNextEnemy()
    {
        currentEnemy = enemyQueue.Dequeue();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static bool isPlayerPhase = true;

    public static void makePlayerPhase()
    {
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
        FindObjectOfType<TurnManager>().clearTileLists();
        foreach (GameObject t in FindObjectOfType<MapData>().tiles)
        {
            t.GetComponent<Tile>().Reset();
        }
        foreach (GameObject character in FindObjectOfType<MapData>().getEnemies())
        {
            character.GetComponent<EnemyMove>().refresh();
        }
        isPlayerPhase = false;
        //Refresh enemies
    }

    public void Update()
    {
        if (isPlayerPhase)
        {
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
            character.GetComponent<EnemyMove>().removeEdgeTiles();
            character.GetComponent<EnemyMove>().removeStaffableTiles();
            character.GetComponent<EnemyMove>().removeAttackableTiles();
            character.GetComponent<EnemyMove>().removeSelectableTiles();
        }
    }
}

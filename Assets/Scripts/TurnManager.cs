using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static bool isPlayerPhase = true;

    public static void makePlayerPhase()
    {
        isPlayerPhase = true;
        foreach (GameObject player in FindObjectOfType<MapData>().getPlayers())
        {
            player.GetComponent<PlayerCharacter>().refresh();
        }
    }

    public static void makeEnemyPhase()
    {
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
                Debug.Log("all players moved");
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
                Debug.Log("all enemies moved");
                makePlayerPhase();
            }


        }
    }
}

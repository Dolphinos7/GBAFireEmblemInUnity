using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPlayer : MonoBehaviour
{
    private GameObject cursor;
    private MapData map;
    GameObject standingOn;
    CharacterStats stats;
    List<GameObject> movementTiles;

    void Start()
    {
        map = GameObject.Find("GameMaster").GetComponent<MapData>();
        cursor = GameObject.Find("Cursor");
        //stats = gameObject.GetComponent<HectorInfo>().getHectorStats();
        movementTiles = new List<GameObject>();
    }
    void Update()
    {

        if (cursor.GetComponent<Transform>().position == transform.position && Input.GetMouseButtonDown(0))
        {
            if (gameObject.GetComponent<HectorInfo>().getHectorStats().getCanMove())
            {
                //Highlight movable tiles in blue
                //Highlight attackable tiles in red
                //Highlight healable tiles in green
                //Run pathfinding algorithm and draw arrow until move is selected
                gameObject.GetComponent<HectorInfo>().getHectorStats().setCanMove(false);
            }
            GameObject.Find("GameMaster").GetComponent<UIController>().displayPopupMenu();
            GameObject.Find("GameMaster").GetComponent<UIController>().disableCursor();

        }

    }

    
    /*
    ublic void scanForMovableTiles(){
        standingOn = map.getGrid().getGridArray()[(int)(transform.position.x + .5), (int)(transform.position.y + .5)];
        scanTiles(stats.getMovement(), standingOn);
    }

    public List<GameObject> scanTiles(int availableMovement, GameObject currentTile){
        if (availableMovement == 0){
            return movementTiles;
        }
        return null;
        

    }
    */


}

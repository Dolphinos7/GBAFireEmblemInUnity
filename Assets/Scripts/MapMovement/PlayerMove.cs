using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : TacticsMove
{


    public bool tilesSelected = false;
    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame


    void Update()
    {
        
/*

        if (TurnManager.isPlayerPhase)
        {





            if (!moving && moveMode)
            {
                if (!tilesSelected)
                {
                    findSelectableTiles();
                    highlightAttackableTiles();
                    highlightStaffableTiles();

                    tilesSelected = true;
                }
                checkMouse();
            }
            else if (moving)
            {
                moveCharacter();
                tilesSelected = false;
            }
        }

        */
    }

    public void checkMouse()
    {
        
        if (Input.GetMouseButtonDown(0) && GameObject.Find("GameMaster").GetComponent<UIController>().getCursor().activeSelf)
        {
            
            Transform cursorTrans = GameObject.Find("GameMaster").GetComponent<UIController>().getCursor().GetComponent<Transform>();
            Collider2D[] colliders = Physics2D.OverlapBoxAll(new Vector2(cursorTrans.position.x, cursorTrans.transform.position.y), new Vector2(.25f, .25f), 0f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.tag == "Terrain")
                {
                    Tile t = collider.GetComponent<Tile>();

                    if (t.selectable)
                    {
                    
                        moveToTile(t);
                        moveMode = false;
                        //GetComponent<PlayerCharacter>().getStats().setCanMove(false);
                    }
                }
            }
            foreach (GameObject t in tiles)
            {
                t.GetComponent<Tile>().HideHighlights();
            }
        }
    }

    public void setMoveMode(bool boolean)
    {
        moveMode = boolean;
    }


}

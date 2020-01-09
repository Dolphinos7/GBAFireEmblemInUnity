using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    public bool inMenu;
    public bool selected;
    private GameObject cursor;
    private PlayerMove moveController;
    public bool itemsMode = false;
    public Vector3 startingPosition;
    public bool hasStartingPosition = false;

    void Start()
    {
        cursor = GameObject.Find("Cursor");
        moveController = GetComponent<PlayerMove>();
        init();
    }

    // Update is called once per frame
    void Update()
    {
        if (waiting)
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("GrayscaleHector");
        }
        else
        {
            GetComponent<Animator>().enabled = true;
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Empty");
        }


        if (TurnManager.isPlayerPhase)
        {

            if (!hasStartingPosition)
            {
                startingPosition = transform.position;
                hasStartingPosition = true;
            }
            //Move into turn manager
            //Debug.Log(moveController.moving);
            //if (cursor.GetComponent<Transform>().position == transform.position && Input.GetMouseButtonUp(0) && !waiting)
            //{
            //selected = true;
            //TurnManager.playerSelected = gameObject;
            //moveController.tilesSelected = false;
            //Debug.Log(moveController.moving);
            //}

            if (TurnManager.playerSelected != gameObject && !waiting)
            {
                transform.position = startingPosition;
                getStats().setCanMove(true);
                moveController.clearAllTileLists();
                selected = false;
                inMenu = false;
                moveController.tilesSelected = false;
                if (TurnManager.playerSelected != null && TurnManager.playerSelected.GetComponent<PlayerCharacter>().inMenu != true)
                {
                    GameObject.Find("GameMaster").GetComponent<UIController>().hidePopupMenu();
                }

            }

            if (selected && getStats().getCanMove() && TurnManager.playerSelected == gameObject)
            {
                if (!moveController.moving)
                {
                    if (!moveController.tilesSelected)
                    {
                        moveController.clearAllTileLists();
                        moveController.findSelectableTiles();
                        moveController.highlightAttackableTiles();
                        moveController.highlightStaffableTiles();

                        moveController.tilesSelected = true;
                    }
                    moveController.checkMouse();

                }
                else if (moveController.moving)
                {
                    GameObject.Find("GameMaster").GetComponent<UIController>().disableCursor();
                    moveController.moveCharacter();
                    moveController.tilesSelected = false;
                }

                else if (!moveController.moving && !itemsMode)
                {

                    //Set Selected To False After Taking Action
                }

            }
            else if (selected && !inMenu && !getStats().getCanMove())
            {
                moveController.clearAllTileLists();
                moveController.moving = false;
                inMenu = true;
                GameObject.Find("GameMaster").GetComponent<UIController>().displayPopupMenu();
                GameObject.Find("GameMaster").GetComponent<UIController>().disableCursor();
            }


        }

    }
    public void setSelected(bool boolean)
    {
        selected = boolean;
    }
    public bool getSelected()
    {
        return selected;
    }

    public void refresh()
    {
        hasStartingPosition = false;
        waiting = false;
        getStats().setCanMove(true);
        GameObject.Find("GameMaster").GetComponent<UIController>().enableCursor();
    }
}

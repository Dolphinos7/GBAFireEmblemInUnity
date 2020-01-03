using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    public bool selected;
    private GameObject cursor;
    private PlayerMove moveController;
    public bool itemsMode = false;

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
        else{
            GetComponent<Animator>().enabled = true;
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Empty");
        }
        if (TurnManager.isPlayerPhase)
        {
            if (cursor.GetComponent<Transform>().position == transform.position && Input.GetMouseButtonUp(0) && getStats().getCanMove())
            {
                selected = true;
            }
            if (selected)
            {
                if (getStats().getCanMove())
                {
                    moveController.setMoveMode(true);
                    //Highlight movable tiles in blue
                    //Highlight attackable tiles in red
                    //Highlight healable tiles in green
                    //Run pathfinding algorithm and draw arrow until move is selected
                }
                else if (!moveController.moving && !itemsMode)
                {
                    GameObject.Find("GameMaster").GetComponent<UIController>().displayPopupMenu();
                    GameObject.Find("GameMaster").GetComponent<UIController>().disableCursor();
                    //Set Selected To False After Taking Action
                }
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

    public void refresh(){
        waiting = false;
        getStats().setCanMove(true);
        GameObject.Find("GameMaster").GetComponent<UIController>().enableCursor();
    }
}

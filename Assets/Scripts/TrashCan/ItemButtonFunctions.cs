using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButtonFunctions : MonoBehaviour
{
    GameObject selectedPlayer;
    PopupMenuFunctions popup;
    Inventory inventory;
    void Start()
    {

    }

    // Update is called once per frame

    void Update()
    {

        Debug.Log("alsoworking");
        ArrayList players = GameObject.Find("GameMaster").GetComponent<MapData>().getPlayers();
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PlayerCharacter>().getSelected())
            {
                selectedPlayer = player;
            }
        }
        popup = GetComponentInParent<PopupMenuFunctions>();
        inventory = selectedPlayer.GetComponent<PlayerCharacter>().inventory;

        popup.setVisibility(false);
        openItemMenu();
        //Close PopupMenu
        //Open Item Submenu

    }


    public void openItemMenu()
    {
        Instantiate(Resources.Load<GameObject>("ItemMenu"));
        Debug.Log("working");
    }
}

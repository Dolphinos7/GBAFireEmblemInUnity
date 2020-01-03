using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitButtonFunctions : MonoBehaviour
{
    GameObject selectedPlayer;
    void Start()
    {
        
    }

    // Update is called once per frame

    
    void Update()   
    {

        if (Input.GetMouseButtonUp(0) && (Input.mousePosition.y > GetComponentInParent<RectTransform>().anchoredPosition.y + 280 - 15 
            && Input.mousePosition.y < GetComponent<RectTransform>().anchoredPosition.y + 280 + 15) && 
                GameObject.Find("PopupMenu").GetComponent<PopupMenuFunctions>().getVisibility())
        {
            Debug.Log("working");
            ArrayList players = GameObject.Find("GameMaster").GetComponent<MapData>().getPlayers();
            foreach (GameObject player in players){
                if (player.GetComponent<PlayerCharacter>().getSelected()){
                    selectedPlayer = player;
                }
            }
            selectedPlayer.GetComponent<PlayerCharacter>().setSelected(false);
            //Update sprite to be greyed out
            GameObject.Find("PopupMenu").GetComponent<PopupMenuFunctions>().setVisibility(false);
            GameObject.Find("PopupMenu").GetComponent<PopupMenuFunctions>().updateVisibility();
            selectedPlayer.GetComponent<PlayerCharacter>().waiting = true;
        }
    }
}

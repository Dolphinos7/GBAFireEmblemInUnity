using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateButtons : MonoBehaviour
{
    PopupMenuFunctions popup;
    Inventory inventory;
    GameObject selectedPlayer;
    public GameObject emptyButton;
    private bool waitVisible;
    private bool itemVisible;
    private bool attackVisible;
    private bool tradeVisible;
    private bool staffVisible;
    private int numButtons;
    void Start()
    {
        numButtons = 0;

        //TEMP CODE FOR TESTING
        waitVisible = true;
        itemVisible = true;
        attackVisible = true;
        tradeVisible = true;
        staffVisible = true;
        createButtons();
        hideButtons();
    }
    public void createButtons()
    {
        if (waitVisible)
        {
            GameObject temp = Instantiate(emptyButton, new Vector3(0, 0, 0), Quaternion.identity);
            temp.transform.SetParent(transform);
            temp.GetComponent<RectTransform>().position = gameObject.GetComponent<RectTransform>().position + new Vector3(0, 120, 0);
            numButtons++;
            temp.GetComponent<Text>().text = "Wait";
            temp.GetComponent<Button>().onClick.AddListener(onWaitButtonClick);
        }
        if (itemVisible)
        {
            GameObject temp = Instantiate(emptyButton, new Vector3(0, 0, 0), Quaternion.identity);
            temp.transform.SetParent(transform);
            temp.GetComponent<RectTransform>().position = gameObject.GetComponent<RectTransform>().position + new Vector3(0, 120, 0) - new Vector3(0, 30 * numButtons, 0);
            numButtons++;
            temp.GetComponent<Text>().text = "Items";
            temp.GetComponent<Button>().onClick.AddListener(onItemButtonClick);
        }
        if (attackVisible)
        {
            GameObject temp = Instantiate(emptyButton, new Vector3(0, 0, 0), Quaternion.identity);
            temp.transform.SetParent(transform);
            temp.GetComponent<RectTransform>().position = gameObject.GetComponent<RectTransform>().position + new Vector3(0, 120, 0) - new Vector3(0, 30 * numButtons, 0);
            numButtons++;
            temp.GetComponent<Text>().text = "Attack";
        }
        if (tradeVisible)
        {
            GameObject temp = Instantiate(emptyButton, new Vector3(0, 0, 0), Quaternion.identity);
            temp.transform.SetParent(transform);
            temp.GetComponent<RectTransform>().position = gameObject.GetComponent<RectTransform>().position + new Vector3(0, 120, 0) - new Vector3(0, 30 * numButtons, 0);
            numButtons++;
            temp.GetComponent<Text>().text = "Trade";
        }
        if (staffVisible)
        {
            GameObject temp = Instantiate(emptyButton, new Vector3(0, 0, 0), Quaternion.identity);
            temp.transform.SetParent(transform);
            temp.GetComponent<RectTransform>().position = gameObject.GetComponent<RectTransform>().position + new Vector3(0, 120, 0) - new Vector3(0, 30 * numButtons, 0);
            numButtons++;
            temp.GetComponent<Text>().text = "Staff";
        }
    }

    public void hideButtons()
    {
        Text[] texts = gameObject.GetComponentsInChildren<Text>();
        for (int i = 0; i < texts.GetLength(0); i++)
        {
            texts[i].enabled = false;
        }
    }

    public void showButtons()
    {
        Text[] texts = gameObject.GetComponentsInChildren<Text>();
        for (int i = 0; i < texts.GetLength(0); i++)
        {
            texts[i].enabled = true;
        }
    }

    public void onWaitButtonClick()
    {
        popup = GameObject.Find("PopupMenu").GetComponent<PopupMenuFunctions>();
        List<GameObject> players = GameObject.Find("GameMaster").GetComponent<MapData>().getPlayers();
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PlayerCharacter>().getSelected())
            {
                selectedPlayer = player;
            }
        }
        selectedPlayer.GetComponent<PlayerCharacter>().setSelected(false);
        //Update sprite to be greyed out
        popup.setVisibility(false);
        popup.updateVisibility();
        selectedPlayer.GetComponent<PlayerCharacter>().waiting = true;

    }

    public void onItemButtonClick()
    {
        popup = GameObject.Find("PopupMenu").GetComponent<PopupMenuFunctions>();
        List<GameObject> players = GameObject.Find("GameMaster").GetComponent<MapData>().getPlayers();
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PlayerCharacter>().getSelected())
            {
                selectedPlayer = player;
            }
        }
        selectedPlayer.GetComponent<PlayerCharacter>().itemsMode = true;
        inventory = selectedPlayer.GetComponent<PlayerCharacter>().inventory;

        popup.setVisibility(false);
        popup.updateVisibility();
        Instantiate(Resources.Load<GameObject>("ItemMenu"));
        //Close PopupMenu
        //Open Item Submenu
    }


}

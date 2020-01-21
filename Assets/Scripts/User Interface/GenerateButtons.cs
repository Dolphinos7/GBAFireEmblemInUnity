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
    private bool waitVisible = false;
    public bool enableCursor;
    private bool itemVisible = false;
    private bool attackVisible = false;
    private bool tradeVisible = false;
    private bool staffVisible = false;
    private bool cancelVisible = false;
    private int numButtons;
    void Start()
    {
        numButtons = 0;

        //TEMP CODE FOR TESTING

    }

    void Update()
    {
        if (enableCursor)
        {
            GameObject.Find("GameMaster").GetComponent<UIController>().enableCursor();
            enableCursor = false;
        }
    }
    public void createButtons()
    {
        numButtons = 0;
        if (waitVisible)
        {
            GameObject temp = Instantiate(emptyButton, new Vector3(0, 0, 0), Quaternion.identity);
            temp.transform.SetParent(transform);
            temp.GetComponent<RectTransform>().position = gameObject.GetComponent<RectTransform>().position + new Vector3(0, 120, 0);
            numButtons++;
            temp.GetComponent<Text>().text = "Wait";
            temp.GetComponent<Button>().onClick.AddListener(onWaitButtonClick);
        }
        if (cancelVisible)
        {
            GameObject temp = Instantiate(emptyButton, new Vector3(0, 0, 0), Quaternion.identity);
            temp.transform.SetParent(transform);
            temp.GetComponent<RectTransform>().position = gameObject.GetComponent<RectTransform>().position + new Vector3(0, 120, 0) - new Vector3(0, 30 * numButtons, 0);
            numButtons++;
            temp.GetComponent<Text>().text = "Cancel";
            temp.GetComponent<Button>().onClick.AddListener(onCancelButtonClick);
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
            Destroy(texts[i]);
        }

        itemVisible = false;
        attackVisible = false;
        tradeVisible = false;
        staffVisible = false;
        cancelVisible = false;

        waitVisible = false;
    }

    public void showButtons()
    {
        ToggleButtonVisibilities();
        createButtons();
        Text[] texts = gameObject.GetComponentsInChildren<Text>();
        for (int i = 0; i < texts.GetLength(0); i++)
        {
            texts[i].enabled = true;
        }
    }

    public void ToggleButtonVisibilities()
    {
        waitVisible = true;
        if (TurnManager.playerSelected.GetComponent<PlayerCharacter>().inventory.Count() > 0)
        {
            itemVisible = true;
        }
        foreach (Tile t in TurnManager.playerSelected.GetComponent<PlayerMove>().attackableTiles)
        {
            Collider2D[] colliders = t.GetOnTopOf();
            foreach (Collider2D collider in colliders)
            {
                if (collider.tag == "Enemy")
                {
                    attackVisible = true;
                }
            }
        }
        Tile tile = TurnManager.playerSelected.GetComponent<PlayerMove>().getTargetTile(TurnManager.playerSelected);
        foreach (Tile t in tile.adjacencyList)
        {
            Collider2D[] colliders = t.GetOnTopOf();
            foreach (Collider2D collider in colliders)
            {
                if (collider.tag == "Player")
                {
                    tradeVisible = true;
                }
            }
        }
        foreach (Tile t in TurnManager.playerSelected.GetComponent<PlayerMove>().staffableTiles)
        {
            Collider2D[] colliders = t.GetOnTopOf();
            foreach (Collider2D collider in colliders)
            {
                if (collider.tag == "Player")
                {
                    staffVisible = true;
                }
            }
        }
        cancelVisible = true;
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
        selectedPlayer.GetComponent<PlayerCharacter>().inMenu = false;
        GameObject.Find("GameMaster").GetComponent<UIController>().enableCursor();

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
        GameObject temp = Instantiate(Resources.Load<GameObject>("ItemMenu"));

        //Close PopupMenu
        //Open Item Submenu
    }

    public void onCancelButtonClick()
    {
        popup = GameObject.Find("PopupMenu").GetComponent<PopupMenuFunctions>();
        List<GameObject> players = GameObject.Find("GameMaster").GetComponent<MapData>().getPlayers();
        selectedPlayer = TurnManager.playerSelected;
        /*
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PlayerCharacter>().getSelected())
            {
                Debug.Log(selectedPlayer);
                selectedPlayer = player;
            }
        }
        */

        selectedPlayer.GetComponent<PlayerCharacter>().setSelected(false);
        popup.setVisibility(false);
        popup.updateVisibility();
        selectedPlayer.GetComponent<PlayerCharacter>().inMenu = false;
        enableCursor = true;
        TurnManager.playerSelected = null;
    }


}

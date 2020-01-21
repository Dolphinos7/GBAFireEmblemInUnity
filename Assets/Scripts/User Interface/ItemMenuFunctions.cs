using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemMenuFunctions : MonoBehaviour
{
    GameObject cursor;
    Inventory inventory;
    public bool hasSlot1 = true;
    public GameObject emptyButton;
    public GameObject ItemStatBox;
    public GameObject ItemDescriptionBox;
    public static Item currentItem = null;
    PopupMenuFunctions popup;
    int numItems;
    void Start()
    {
        popup = GameObject.Find("PopupMenu").GetComponent<PopupMenuFunctions>();
        inventory = TurnManager.playerSelected.GetComponent<PlayerCharacter>().inventory;

        displayInventory();
        cursor = GameObject.Find("Cursor");
        transform.SetParent(GameObject.Find("Canvas").transform);
    }

    // Update is called once per frame
    void Update()
    {
        updatePosition();

        //ON Exit Disable Items Mode To Re-Open Popup Menu using selectedPlayer.GetComponent<PlayerCharacter>().itemsMode = true;
    }

    public void updatePosition()
    {
        if (Input.mousePosition.x > Screen.width / 2)
        {
            GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width / 2 - 100, 0);

        }
        else
        {
            GetComponent<RectTransform>().anchoredPosition = new Vector2(-Screen.width / 2 + 100, 0);
        }
    }

    public void displayInventory()
    {
        for (int i = 0; i < inventory.Count(); i++)
        {
            GameObject temp = Instantiate(emptyButton, new Vector3(0, 0, 0), Quaternion.identity);
            temp.transform.SetParent(transform);
            temp.GetComponent<RectTransform>().position = gameObject.GetComponent<RectTransform>().position + new Vector3(0, 120, 0) - new Vector3(0, 30 * numItems, 0);
            numItems++;
            temp.GetComponent<Text>().text = inventory.Get(i).name;
            //Treating all as weapons for now
                temp.AddComponent<OnItemHover>();
                temp.GetComponent<OnItemHover>().thisItem = inventory.Get(i);

            temp.GetComponent<Button>().onClick.AddListener(itemClicked);
        }

        GameObject backButton = Instantiate(emptyButton, new Vector3(0, 0, 0), Quaternion.identity);
        backButton.transform.SetParent(transform);
        backButton.GetComponent<RectTransform>().position = gameObject.GetComponent<RectTransform>().position + new Vector3(0, 120, 0) - new Vector3(0, 30 * numItems, 0);
        backButton.GetComponent<Text>().text = "Back";
        //Treating all as weapons for now
        backButton.GetComponent<Button>().onClick.AddListener(backClicked);
        //For length of the inventory Done
        //Display each item as a button Done
        //Have a image next to each button
        //Add itemHover component which has OnMouseEnter and OnMouseExit methods
        //When mouse is on the item pull up new window in bottom right
        //Display Attack, Crit, Hit, and Avoid on bottom right
        //on button click call itemClicked()
        /*
        if (hasSlot1){
            GameObject temp = Instantiate(emptyButton, new Vector3(0, 0, 0), Quaternion.identity);
            temp.transform.SetParent(transform);
            temp.GetComponent<RectTransform>().position = gameObject.GetComponent<RectTransform>().position + new Vector3(0, 120, 0) - - new Vector3(0, 30 * numItems, 0);
            numItems++;
            temp.GetComponent<Text>().text = "ItemName";
        }
        */
    }

    public void backClicked()
    {
        Debug.Log("back");
        popup.setVisibility(true);
        popup.updateVisibility();
        Destroy(gameObject);
    }
    public void itemClicked()
    {
        Debug.Log(currentItem);
        //Bring up new submenu
        //Show equip and discard if a weapon or staff
        //Show use or discard for usable
    }
}

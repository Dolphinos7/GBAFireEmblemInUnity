using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenuFunctions : MonoBehaviour
{
    GameObject cursor;
    Inventory inventory;
    public bool hasSlot1 = true;
    public GameObject emptyButton;
    int numItems;
    void Start()
    {
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

    public void displayInventory(){
        //For length of the inventory
        //Display each item as a button
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

    public void itemClicked(){
        //Bring up new submenu
        //Show equip and discard if a weapon or staff
        //Show use or discard for usable
    }
}

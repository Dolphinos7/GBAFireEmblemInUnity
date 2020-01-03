using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenuFunctions : MonoBehaviour
{
    GameObject cursor;
    Inventory inventory;
    void Start()
    {
        cursor = GameObject.Find("Cursor");
    }

    // Update is called once per frame
    void Update()
    {
        updatePosition();
        //ON Exit Disable Items Mode To Re-Open Popup Menu using selectedPlayer.GetComponent<PlayerCharacter>().itemsMode = true;
    }

    public void updatePosition()
    {
        if (GetComponentInParent<Transform>().position.x > 0)
        {
            GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width / 2 - 100, 0);

        }
        else
        {
            GetComponent<RectTransform>().anchoredPosition = new Vector2(-Screen.width / 2 + 100, 0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    private PopupMenuFunctions popupMenuFunctions;
    private GameObject cursor;
    void Start()
    {
        cursor = GameObject.Find("Cursor");
        popupMenuFunctions = GameObject.Find("Canvas").GetComponentInChildren<PopupMenuFunctions>();
    }

    // Update is called once per frame

    public void displayPopupMenu()
    {
        popupMenuFunctions.setVisibility(true);
        GameObject.Find("Canvas").GetComponentInChildren<PopupMenuFunctions>().updateVisibility();
    }
    

    public void hidePopupMenu()
    {
        popupMenuFunctions.setVisibility(false);
        GameObject.Find("Canvas").GetComponentInChildren<PopupMenuFunctions>().updateVisibility();
    }

    public void disableCursor()
    {
        cursor.SetActive(false);
    }
    public void enableCursor()
    {
        cursor.SetActive(true);
    }

    public GameObject getCursor()
    {
        return cursor;
    }

    void OnMouseDown()
    {
        //Below Code has menu appear on clicking any terrain tile
        /*
        if (flipper)
        {
            popupMenuFunctions.setVisibility(true);
            GameObject.Find("Canvas").GetComponentInChildren<PopupMenuFunctions>().updateVisibility();
            flipper = false;
        }
        else
        {
            popupMenuFunctions.setVisibility(false);
            GameObject.Find("Canvas").GetComponentInChildren<PopupMenuFunctions>().updateVisibility();
            flipper = true;
        }
        */
    }

    public void waitButtonClick(){
        Debug.Log("working");
    }
}
